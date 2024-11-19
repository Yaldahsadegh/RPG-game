using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject
{
    public class CommonItemFactory : IItemFactory
    {
        public Weapon CreateWeapon(string name)
        {
            return new Weapon(name, ItemRarity.Common, 5, WeaponTypeEnum.Melee); // Example values
        }

        public Potion CreatePotion(string name)
        {
            return new Potion(name, ItemRarity.Common, "Heal", 30);
        }

        public Armor CreateArmor(string name)
        {
            return new Armor(name, ItemRarity.Common, 10, 50);
        }
    }
}
