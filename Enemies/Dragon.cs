using RPGproject.CombatSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.Enemies
{
    public class Dragon : Enemy
    {
        public Dragon(EnemyRank rank) : base(rank) { }

        protected override void InitializeStatsByRank(EnemyRank rank)
        {
            Name = rank == EnemyRank.Boss ? "Ancient Dragon" : "Dragon";
            MaxHealth = rank == EnemyRank.Boss ? 1000 : 500;
            Health = MaxHealth;
            Mana = 200;
            Strength = rank == EnemyRank.Boss ? 100 : 50;
            Agility = rank == EnemyRank.Boss ? 30 : 15;
            Defense = rank == EnemyRank.Boss ? 20 : 10;
        }

        public override void Attack(ICombatant target)
        {
            Console.WriteLine($"{Name} breathes fire on {target.Name}!");
            base.Attack(target);
        }

        public override void Move()
        {
            Console.WriteLine($"{Name} flies majestically.");
        }
    }
}
