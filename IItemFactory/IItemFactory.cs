using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject
{
    public interface IItemFactory
    {
        Weapon CreateWeapon(string name);
        Potion CreatePotion(string name);
        Armor CreateArmor(string name);
    }
}