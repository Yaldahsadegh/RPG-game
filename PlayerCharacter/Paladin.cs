using RPGproject.quest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace RPGproject
    {
        public class Paladin : Character, IObserver
        {
            public Paladin(string name) : base(name)
            {
                Health = 150;
                Mana = 50;
                Strength = 100;
                Agility = 30;
            }

            public override void DisplayInfo()
            {
                Console.WriteLine($"Paladin: {Name}, Health: {Health}, Mana: {Mana}, Strength: {Strength}, Agility: {Agility}");
            }

            // Implement the Update method from IObserver
            public void Update(string questStatus)
            {
                Console.WriteLine($"Paladin {Name} received quest notification: {questStatus}");
            }
        }
    }
