using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGproject.CombatSystem;


namespace RPGproject.Enemies
{
    public abstract class Enemy : ICombatant
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; } // Maximum health for consistency
        public int Mana { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Defense { get; set; } // Defense value for reducing damage
        public EnemyRank Rank { get; set; }
        public IActionStrategy ActionStrategy { get; set; } // Each enemy can have a combat strategy
        public ICharacterState CurrentState { get; set; } // Current state of the enemy (e.g., Idle, Attacking)

        // Optional: Equipped gear for enemies
        public Weapon EquippedWeapon { get; set; }
        public Armor EquippedArmor { get; set; }

        public Enemy()
        {
            // Default action strategy is attack; customize based on enemy type
            ActionStrategy = new DefaultAction();
            CurrentState = new IdleState(); // Default state
            MaxHealth = 100; // Default max health
            Health = MaxHealth;
            Defense = 5; // Default defense value
        }

        public void SetActionStrategy(IActionStrategy actionStrategy)
        {
            ActionStrategy = actionStrategy;
        }

        public void SetState(ICharacterState state)
        {
            CurrentState = state;
        }

        public void PerformAction(ICombatant target)
        {
            // Perform an action based on the current strategy
            if (ActionStrategy != null && target != null)
            {
                ActionStrategy.PerformAction(this, target); // Execute the action strategy
            }
            else
            {
                Console.WriteLine($"{Name} cannot perform the action due to missing strategy or target.");
            }
        }

        // Enhanced attack method for all enemies
        public virtual void Attack(ICombatant target)
        {
            if (target == null) return;

            int damage = Strength;

            // Add weapon damage if equipped
            if (EquippedWeapon != null)
            {
                damage += EquippedWeapon.Damage;
            }

            // Calculate reduced damage based on the target's defense
            if (target is Character characterTarget)
            {
                damage -= characterTarget.Defense;
            }
            else if (target is Enemy enemyTarget)
            {
                damage -= enemyTarget.Defense;
            }

            // Ensure damage is not negative
            damage = Math.Max(damage, 0);

            Console.WriteLine($"{Name} attacks {target.Name} for {damage} damage!");
            target.TakeDamage(damage);
        }

        // Take damage method for the enemy
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
                Console.WriteLine($"{Name} takes {damage} damage. Remaining Health: {Health}/{MaxHealth}");
            }
        }

        // Allow the enemy to heal, respecting max health
        public void Heal()
        {
            const int healAmount = 10; // Define a fixed healing amount

            if (Health <= 0)
            {
                Console.WriteLine($"{Name} cannot heal because they are incapacitated.");
                return;
            }

            int newHealth = Health + healAmount;
            if (newHealth > MaxHealth)
            {
                newHealth = MaxHealth; // Cap at max health
            }

            Health = newHealth; // Update health
            Console.WriteLine($"{Name} heals for {healAmount} health. Current health: {Health}/{MaxHealth}");
        }

        // Allow enemies to equip weapons
        public void EquipWeapon(Weapon weapon)
        {
            if (EquippedWeapon != null)
            {
                Console.WriteLine($"{Name} unequips {EquippedWeapon.Name}.");
            }

            EquippedWeapon = weapon;
            Console.WriteLine($"{Name} equips weapon: {weapon.Name}.");
        }

        // Allow enemies to equip armor
        public void EquipArmor(Armor armor)
        {
            if (EquippedArmor != null)
            {
                Defense -= EquippedArmor.Defense;
                Console.WriteLine($"{Name} unequips {EquippedArmor.Name}. Defense decreases by {EquippedArmor.Defense}.");
            }

            EquippedArmor = armor;
            Defense += armor.Defense;
            Console.WriteLine($"{Name} equips armor: {armor.Name}. Defense increases by {armor.Defense}.");
        }

        public abstract void Move(); // Common movement behavior for enemies
    }
}
