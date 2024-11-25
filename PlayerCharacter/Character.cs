using RPGproject.ActionStrategy;
using RPGproject.PlayerCharacter.Commands;
using RPGproject.quest;
using System;
using System.Collections.Generic;
using RPGproject.InventoryManagement;
using RPGproject.CombatSystem;
using RPGproject.Enemies;


namespace RPGproject
{
    public abstract class Character : IObserver, ICombatant

    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Mana { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Defense { get; set; }

        public IActionStrategy ActionStrategy { get; set; } // Action strategy for this character
        public ICharacterState CurrentState { get; set; } // Current state of the character

        // Use Inventory class instead of List<Item>
        public Inventory Inventory { get; private set; } = new Inventory();
        public Weapon EquippedWeapon { get; set; }
        public Armor EquippedArmor { get; set; }
        public Potion EquippedPotion { get; set; }

        protected Character(string name)
        {
            Name = name;
            Health = 100;  // Default health
            MaxHealth = 100; // Default max health
            Mana = 50;     // Default mana
            Strength = 10; // Default strength
            Agility = 10;  // Default agility
            Defense = 0;
            CurrentState = new IdleState(); // Default state
            ActionStrategy = new DefaultAction(); // Default action strategy

            // Subscribe to QuestManager for quest notifications
            QuestManager.Instance.Subscribe(this);
        }

            // Accept a quest via QuestManager
        public void AcceptQuest(string title)
        {
            QuestManager.Instance.AcceptQuest(title, this);  // Accept the quest through QuestManager
        }

        // Complete a quest when the enemy is defeated
        public void CompleteQuest(string title, string defeatedEnemy)
        {
            QuestManager.Instance.CompleteQuest(title, defeatedEnemy, this);  // Complete quest through QuestManager
        }

        // Respond to quest updates from QuestManager
        public void Update(string questStatus)
        {
            Console.WriteLine($"{Name} received a quest update: {questStatus}");
        }

        public abstract void DisplayInfo();


        public void SetActionStrategy(IActionStrategy actionStrategy)
        {
            ActionStrategy = actionStrategy;
        }

        public void SetState(ICharacterState state)
        {
            CurrentState = state;
        }

        public void PerformCurrentState()
        {
            CurrentState.HandleState(this); // Handle current state behavior
        }

        // Implement ICombatant's PerformAction for combat actions
        public void PerformAction(ICombatant target)
        {
            if (ActionStrategy != null && target != null)
            {
                ActionStrategy.PerformAction(this, target); // Perform action based on current strategy
            }
            else
            {
                Console.WriteLine($"{Name} cannot perform the action due to missing strategy or target.");
            }
        }

        // Implementing Defend method
        public void Defend()
        {
            Console.WriteLine($"{Name} is defending.");
            // Increase the defense temporarily (can be a fixed value or multiplier)
            int originalDefense = Defense;
            Defense += 5;  // Example: Add 5 defense points during defense

            // Perform the defensive action (no target needed here)
            SetActionStrategy(new DefendAction()); // Change to DefendAction strategy
            PerformAction(this);  // Defend doesn't need a target, so we pass this character itself

            // Reset defense back to original value after the turn
            Defense = originalDefense;
        }

        // Method for healing
        public void Heal()
        {
            const int healAmount = 10; // Define a fixed healing amount

            if (Health <= 0)
            {
                Console.WriteLine($"{Name} cannot heal because they are incapacitated.");
                return;
            }

            // Calculate new health
            int newHealth = Health + healAmount;

            // Ensure the new health does not exceed the maximum health
            if (newHealth > MaxHealth)
            {
                newHealth = MaxHealth; // Cap at max health
            }

            Health = newHealth; // Update character's health
            Console.WriteLine($"{Name} heals for {healAmount} health. Current health: {Health}/{MaxHealth}");
        }


        // Perform attack action on a target (ICombatant)
        // For the Character class
        public void Attack(ICombatant target)
        {
            if (target == null) return;

            int damage = Strength;

            // If equipped with a weapon, add its damage
            if (EquippedWeapon != null)
            {
                damage += EquippedWeapon.Damage;
            }

            // Calculate damage after considering the target's defense (e.g., Armor)
            if (target is Character targetCharacter)
            {
                damage -= targetCharacter.Defense;  // Subtract the defense of the target (character)
            }
            else if (target is Enemy targetEnemy)
            {
                damage -= targetEnemy.Defense;  // Subtract the defense of the target (enemy)
            }

            // Ensure damage is not negative
            damage = Math.Max(damage, 0);

            Console.WriteLine($"{Name} attacks {target.Name} for {damage} damage!");
            target.TakeDamage(damage);
        }


        // Handle the target taking damage and updating health
        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Health = 0;
                Console.WriteLine($"{Name} has been defeated!");
            }
            else
            {
                Console.WriteLine($"{Name} takes {damage} damage. Remaining Health: {Health}");
            }
        }

        // Inventory and item handling methods
        public void AddItemToInventory(Item item)
        {
            Inventory.AddItemToInventory(item);
            Console.WriteLine($"{item.Name} has been added to {Name}'s inventory.");
        }

        public void UseItem(string itemName)
        {
            var item = Inventory.GetItemByName(itemName);
            if (item != null)
            {
                EquipItem(item);
                Inventory.RemoveItemFromInventory(item);
                Console.WriteLine($"{item.Name} has been used and removed from {Name}'s inventory.");
            }
            else
            {
                Console.WriteLine($"Item {itemName} not found in inventory.");
            }
        }

        private void EquipItem(Item item)
        {
            switch (item.SlotType)
            {
                case EquipmentSlotType.Weapon when item is Weapon weapon:
                    EquipWeapon(weapon);
                    break;
                case EquipmentSlotType.Defensive when item is Armor armor:
                    EquipArmor(armor);
                    break;
                case EquipmentSlotType.Utility when item is Potion potion:
                    EquipPotion(potion);
                    break;
                default:
                    Console.WriteLine($"Item {item.Name} cannot be equipped in the correct slot.");
                    break;
            }
        }

        public void EquipWeapon(Weapon weapon)
        {
            if (EquippedWeapon != null)
            {
                Console.WriteLine($"Unequipping {EquippedWeapon.Name}...");
            }

            EquippedWeapon = weapon;
            Console.WriteLine($"Equipped weapon: {weapon.Name}");
        }

        public void EquipArmor(Armor armor)
        {
            if (EquippedArmor != null)
            {
                // Unequip the current armor and subtract its defense value
                Defense -= EquippedArmor.Defense;
                Console.WriteLine($"Unequipping {EquippedArmor.Name}. Defense decreased by {EquippedArmor.Defense}. Current defense: {Defense}");
            }

            EquippedArmor = armor;
            Defense += armor.Defense;  // Add the new armor's defense value to the character's defense
            Console.WriteLine($"Equipped armor: {armor.Name}. Defense increased by {armor.Defense}. Current defense: {Defense}");
        }

        public void EquipPotion(Potion potion)
        {
            if (EquippedPotion != null)
            {
                Console.WriteLine($"Unequipping {EquippedPotion.Name}...");
            }

            EquippedPotion = potion;
            Console.WriteLine($"Equipped potion: {potion.Name}");
        }

        public void ViewInventory()
        {
            Console.WriteLine($"{Name}'s Inventory:");
            Inventory.ListItems();
        }

        public void RemoveItemFromInventory(string itemName)
        {
            var item = Inventory.GetItemByName(itemName);
            if (item != null)
            {
                Inventory.RemoveItemFromInventory(item);
                Console.WriteLine($"{item.Name} has been removed from {Name}'s inventory.");
            }
            else
            {
                Console.WriteLine($"Item {itemName} not found in inventory.");
            }
        }
    }
}
