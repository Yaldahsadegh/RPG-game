using RPGproject.CombatSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.Enemies
{
    public class Slime : Enemy
    {
        public Slime(EnemyRank rank) : base(rank) { }

        protected override void InitializeStatsByRank(EnemyRank rank)
        {
            Name = rank == EnemyRank.Boss ? "King Slime" : rank == EnemyRank.Elite ? "Elite Slime" : "Slime";
            MaxHealth = rank == EnemyRank.Boss ? 200 : rank == EnemyRank.Elite ? 100 : 50;
            Health = MaxHealth;
            Mana = 10;
            Strength = rank == EnemyRank.Boss ? 20 : rank == EnemyRank.Elite ? 10 : 5;
            Agility = 5;
            Defense = rank == EnemyRank.Boss ? 5 : rank == EnemyRank.Elite ? 3 : 1;
        }

        public override void Attack(ICombatant target)
        {
            Console.WriteLine($"{Name} slaps with a slimy tendril at {target.Name}!");
            base.Attack(target);
        }

        public override void Move()
        {
            Console.WriteLine($"{Name} slithers slowly.");
        }
    }
}
