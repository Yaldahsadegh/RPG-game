using RPGproject.CombatSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject
{

    public class ItemAction : IActionStrategy
    {
        private readonly string _itemName;

        // Constructor to take the name of the item to be used
        public ItemAction(string itemName)
        {
            _itemName = itemName;
        }

        public void PerformAction(ICombatant actor, ICombatant target)  // Add target parameter
        {
            // Find the item from the actor's inventory
            var item = (actor as Character)?.Inventory.GetItemByName(_itemName);

            if (item == null)
            {
                Console.WriteLine($"{actor.Name} does not have {item.Name} in their inventory.");
                return;
            }

            // Equip or use the item based on its type
            EquipItem(actor as Character, item, target);  // Pass target to EquipItem
        }

        private void EquipItem(Character character, Item item, ICombatant target)  // Added target as parameter
        {
            switch (item.SlotType)
            {
                case EquipmentSlotType.Weapon:
                    EquipWeapon(character, item as Weapon, target);  // Pass target to EquipWeapon
                    break;
                case EquipmentSlotType.Defensive:
                    EquipArmor(character, item as Armor, target);  // Pass target to EquipArmor
                    break;
                case EquipmentSlotType.Utility:
                    EquipPotion(character, item as Potion, target);  // Pass target to EquipPotion
                    break;
                default:
                    Console.WriteLine($"Item {item.Name} cannot be equipped in the correct slot.");
                    break;
            }
        }

        private void EquipWeapon(Character character, Weapon weapon, ICombatant target)
        {
            if (weapon != null)
            {
                character.EquipWeapon(weapon);  // Equip the weapon
                Console.WriteLine($"{character.Name} has equipped {weapon.Name}. Damage: {weapon.Damage}");
            }
        }

        private void EquipArmor(Character character, Armor armor, ICombatant target)
        {
            if (armor != null)
            {
                character.EquipArmor(armor);  // Equip the armor
                                              // Increase defense based on the armor stats
                character.Defense += armor.Defense;
                Console.WriteLine($"{character.Name} has equipped {armor.Name}. Defense: {armor.Defense}");
            }
        }

        private void EquipPotion(Character character, Potion potion, ICombatant target)
        {
            if (potion != null)
            {
                character.EquipPotion(potion);  // Equip the potion
                if (potion.Effect.Equals("Healing", StringComparison.OrdinalIgnoreCase))
                {
                    character.Heal();  // Call heal method in Character class
                }
                Console.WriteLine($"{character.Name} has used {potion.Name}.");
            }
        }
    }

}
