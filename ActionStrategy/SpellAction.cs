using RPGproject.CombatSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject
{
    public class SpellAction : IActionStrategy
    {
        public void PerformAction(ICombatant actor, ICombatant target)  // Added target parameter
        {
            Console.WriteLine($"{actor.Name} casts a magic spell on {target.Name}!");
            // Additional logic for spell casting can be added here, such as targeting the enemy and applying effects
        }
    }
}
