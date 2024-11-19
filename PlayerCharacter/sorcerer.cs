using System;
using RPGproject.quest;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject
{
    public class Sorcerer : Character, IObserver
    {
        public Sorcerer(string name) : base(name)
        {
            Health = 80;
            Mana = 120;
            Strength = 30;
            Agility = 50;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Sorcerer: {Name}, Health: {Health}, Mana: {Mana}, Strength: {Strength}, Agility: {Agility}");
        }
        // Implement the Update method from IObserver
        public void Update(string questStatus)
        {
            Console.WriteLine($"Sorcerer {Name} received quest notification: {questStatus}");
        }
    }
}
