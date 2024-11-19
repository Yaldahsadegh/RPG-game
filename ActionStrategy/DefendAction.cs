using RPGproject.CombatSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.ActionStrategy
{
    public class DefendAction : IActionStrategy
    {
        public void PerformAction(ICombatant actor, ICombatant target)
        {
            // Defensive stance applies only to the actor
            Console.WriteLine($"{actor.Name} assumes a defensive stance.");
            actor.Health += 5; // Example: Adds temporary health or damage reduction
        }
    }
}
