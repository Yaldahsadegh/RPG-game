using RPGproject.CombatSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject
{
    public class AttackAction : IActionStrategy
    {
        public void PerformAction(ICombatant actor, ICombatant target)
        {
            Console.WriteLine($"{actor.Name} attacks {target.Name}!");

            // Example damage calculation (consider Strength and equipment in the future)
            int damage = actor.Strength;
            target.TakeDamage(damage);

            Console.WriteLine($"{target.Name} takes {damage} damage! Remaining health: {target.Health}");
        }
    }
}
