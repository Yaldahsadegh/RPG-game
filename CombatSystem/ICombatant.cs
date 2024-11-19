using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.CombatSystem
{
    public interface ICombatant
    {
        string Name { get; }
        int Health { get; set; }
        int Mana { get; set; }
        int Strength { get; set; }
        void Attack(ICombatant target);
        void TakeDamage(int damage);
    }
}
