using RPGproject.quest;
using System;

namespace RPGproject
{
    public class NPC : IObserver
    {
        public string Name { get; set; }
        public string Role { get; set; }

        public NPC(string name, string role)
        {
            Name = name;
            Role = role;
        }

        // Implement the Update method from IObserver
        public void Update(string questStatus)
        {
            Console.WriteLine($"{Name} the {Role} received a quest update: {questStatus}");
        }
    }
}

