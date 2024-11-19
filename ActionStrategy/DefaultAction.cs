using RPGproject.CombatSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject
{
    public class DefaultAction : IActionStrategy
    {
        public void PerformAction(ICombatant actor, ICombatant target)
        {
            Console.WriteLine($"{actor.Name} is standing still, observing.");

            // Example: Maybe characters regain some mana or energy while doing nothing.
            actor.Mana += 5; // Regenerate a small amount of mana while in default state.
            Console.WriteLine($"{actor.Name} regenerates 5 Mana. Current Mana: {actor.Mana}");
        }
    }
}
