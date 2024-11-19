using RPGproject.quest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RPGproject
{
    public class CharacterFactory
    {
        public static Character CreateCharacter(string type, string name)
        {
            Character newCharacter;

            switch (type.ToLower())
            {
                case "paladin":
                    newCharacter = new Paladin(name);
                    break;
                case "sorcerer":
                    newCharacter = new Sorcerer(name);
                    break;
                case "ranger":
                    newCharacter = new Ranger(name);
                    break;
                default:
                    throw new ArgumentException("Invalid character type");
            }

            // Subscribe the new character to the QuestManager
            QuestManager.Instance.Subscribe(newCharacter);

            return newCharacter;
        }
    }
}