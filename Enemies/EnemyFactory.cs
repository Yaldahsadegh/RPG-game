using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.Enemies
{
    public static class EnemyFactory
    {
        public static Enemy CreateEnemy(string type, EnemyRank rank) // Make the method static
        {
            if (type == "Slime")
                return new Slime(rank);
            else if (type == "Goblin")
                return new Goblin(rank);
            else if (type == "Dragon")
                return new Dragon(rank);
            else
                throw new ArgumentException("Invalid enemy type");
        }
    }
}
