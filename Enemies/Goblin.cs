using RPGproject.CombatSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.Enemies
{
    public class Goblin : Enemy
    {
        public Goblin(EnemyRank rank) : base(rank) { }

        protected override void InitializeStatsByRank(EnemyRank rank)
        {
            Name = rank == EnemyRank.Boss ? "Goblin King" : rank == EnemyRank.Elite ? "Goblin Elite" : "Goblin";
            MaxHealth = rank == EnemyRank.Boss ? 150 : rank == EnemyRank.Elite ? 120 : 80;
            Health = MaxHealth;
            Mana = 20;
            Strength = rank == EnemyRank.Boss ? 30 : rank == EnemyRank.Elite ? 20 : 10;
            Agility = 10;
            Defense = rank == EnemyRank.Boss ? 8 : rank == EnemyRank.Elite ? 6 : 4;
        }

        public override void Attack(ICombatant target)
        {
            Console.WriteLine($"{Name} swings a club at {target.Name}!");
            base.Attack(target);
        }

        public override void Move()
        {
            Console.WriteLine($"{Name} scurries around.");
        }
    }
}
