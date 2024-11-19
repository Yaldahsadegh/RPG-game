using RPGproject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject
{
    public abstract class Item
    {
        public string Name { get; set; }
        public ItemRarity Rarity { get; set; }
        public EquipmentSlotType SlotType { get; set; }  // New property to indicate which slot it fits into

        protected Item(string name, ItemRarity rarity,  EquipmentSlotType slotType)
        {
            Name = name;
            Rarity = rarity;
            SlotType = slotType;
        }
    }
}
public class Weapon : Item
{
    public int Damage { get; set; }
    public WeaponTypeEnum WeaponType { get; set; }

    public Weapon(string name, ItemRarity rarity, int damage, WeaponTypeEnum weaponType)
        : base(name, rarity, EquipmentSlotType.Weapon) // Passing the correct SlotType to the base constructor
    {
        Damage = damage;
        WeaponType = weaponType;
    }
}
public class Potion : Item
{
    public string Effect { get; set; }
    public int Duration { get; set; }

    public Potion(string name, ItemRarity rarity, string effect, int duration)
        : base(name, rarity, EquipmentSlotType.Utility) // Set slot type to Utility
    {
        Effect = effect;
        Duration = duration;
    }
}
public class Armor : Item
{
    public int Defense { get; set; }
    public int Durability { get; set; }

    public Armor(string name, ItemRarity rarity, int defense, int durability)
        : base(name, rarity, EquipmentSlotType.Defensive) // Set slot type to Defensive
    {
        Defense = defense;
        Durability = durability;
    }
}

