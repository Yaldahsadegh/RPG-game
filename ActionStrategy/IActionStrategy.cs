using RPGproject.CombatSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject
{
    public interface IActionStrategy
    {
        void PerformAction(ICombatant actor, ICombatant target);
    }
}
