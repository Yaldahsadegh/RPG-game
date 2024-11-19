using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject
{
    public class RareItemFactory : IItemFactory
    {
        public Weapon CreateWeapon(string name)
        {
            return new Weapon(name, ItemRarity.Rare, 25, WeaponTypeEnum.Ranged); // Example values
        }

        public Potion CreatePotion(string name)
        {
            return new Potion(name, ItemRarity.Rare, "Greater Heal", 90);
        }

        public Armor CreateArmor(string name)
        {
            return new Armor(name, ItemRarity.Rare, 30, 100);
        }
    }
}
