using System;
using RPGproject.quest;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject
{
    public class Ranger : Character, IObserver
    {
        public Ranger(string name) : base(name)
        {
            Health = 100;
            Mana = 40;
            Strength = 50;
            Agility = 100;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Ranger: {Name}, Health: {Health}, Mana: {Mana}, Strength: {Strength}, Agility: {Agility}");
        }
        // Implement the Update method from IObserver
        public void Update(string questStatus)
        {
            Console.WriteLine($"Ranger {Name} received quest notification: {questStatus}");
        }
    }
}
