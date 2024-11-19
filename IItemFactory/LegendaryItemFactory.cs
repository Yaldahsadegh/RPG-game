using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject
{
    public class LegendaryItemFactory : IItemFactory
    {
        public Weapon CreateWeapon(string name)
        {
            return new Weapon(name, ItemRarity.Legendary, 50, WeaponTypeEnum.Ranged); // Example values
        }

        public Potion CreatePotion(string name)
        {
            return new Potion(name, ItemRarity.Legendary, "Full Restoration", 120);
        }

        public Armor CreateArmor(string name)
        {
            return new Armor(name, ItemRarity.Legendary, 50, 200);
        }
    }
}