using System;
using System.Collections.Generic;
using System.Linq;
using RPGproject.Enemies;

namespace RPGproject.quest
{
    public class QuestManager : ISubject
    {
        private static QuestManager instance;
        private List<IObserver> observers = new List<IObserver>();
        private List<Quest> quests = new List<Quest>();

        // Dictionary to track which quests each character has accepted
        private Dictionary<Character, List<Quest>> characterQuests = new Dictionary<Character, List<Quest>>();

        // Singleton pattern
        public static QuestManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new QuestManager();
                }
                return instance;
            }
        }

        public void Subscribe(IObserver observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
        }

        public void Unsubscribe(IObserver observer)
        {
            if (observers.Contains(observer))
            {
                observers.Remove(observer);
            }
        }

        public void Notify(string questStatus)
        {
            foreach (var observer in observers)
            {
                observer.Update(questStatus);
            }
        }

        // Start a quest assigned by an NPC, related to a specific enemy
        public void StartQuestForDefeatingEnemy(NPC assignedNPC, string enemyName)
        {
            string title = $"Defeat the {enemyName}";
            string description = $"Defeat a {enemyName} to help {assignedNPC.Name}.";

            var quest = new Quest(title, description, assignedNPC, enemyName);
            quests.Add(quest);


            // Notify observers about the quest
            Notify($"Quest Available: {quest.Title} - {quest.Description}");
        }

        // Accept a quest if not already accepted
        public void AcceptQuest(string title, Character character)
        {
            var quest = quests.FirstOrDefault(q => q.Title == title);
            if (quest != null)
            {
                if (quest.Status == QuestStatus.NotAccepted)
                {
                    quest.Status = QuestStatus.InProgress;
                    Console.WriteLine($"Quest '{quest.Title}' is now in progress.");

                    // Add the quest to the character's active quest log
                    if (!characterQuests.ContainsKey(character))
                    {
                        characterQuests[character] = new List<Quest>();
                    }
                    characterQuests[character].Add(quest);  // Track quest for the character

                    Notify($"Quest Accepted: {quest.Title} by {character.Name}");
                }
                else
                {
                    Console.WriteLine($"Quest '{quest.Title}' is already {quest.Status}.");
                }
            }
            else
            {
                Console.WriteLine($"Quest '{title}' not found.");
            }
        }

        // Deny a quest, marking it as Denied
        public void DenyQuest(string title)
        {
            var quest = quests.FirstOrDefault(q => q.Title == title);
            if (quest != null && quest.Status == QuestStatus.NotAccepted)
            {
                quest.Status = QuestStatus.Denied;
                Console.WriteLine($"Quest '{quest.Title}' has been denied.");
                Notify($"Quest Denied: {quest.Title}");
            }
            else
            {
                Console.WriteLine($"Quest '{title}' not found or already accepted.");
            }
        }

        // Complete a quest when the assigned enemy is defeated
        public void CompleteQuest(string title, string defeatedEnemy, Character character)
        {
            var quest = characterQuests.ContainsKey(character) ?
                        characterQuests[character].FirstOrDefault(q => q.Title == title && q.Status == QuestStatus.InProgress) : null;

            if (quest != null && quest.EnemyName == defeatedEnemy)
            {
                quest.Status = QuestStatus.Completed;
                Notify($"Quest Completed: {quest.Title} - {defeatedEnemy} defeated!");
                Console.WriteLine($"Quest '{quest.Title}' is now completed for {character.Name}.");
            }
            else
            {
                Console.WriteLine($"Quest '{title}' is not in progress or the enemy does not match for {character.Name}.");
            }
        }

        // Method to verify if an enemy exists in the game world
        public bool VerifyEnemyExists(string enemyName, GameWorld gameWorld)
        {
            return gameWorld.Enemies.Any(e => e.GetType().Name == enemyName);
        }

        // Get all quests for viewing
        public List<Quest> GetQuests()
        {
            return quests; // Return the list of quests for viewing
        }

        // Method to get a character's active quests
        public List<Quest> GetCharacterQuests(Character character)
        {
            return characterQuests.ContainsKey(character) ? characterQuests[character] : new List<Quest>();
        }
    }

    // QuestStatus Enum for more structured quest states
    public enum QuestStatus
    {
        NotAccepted,
        InProgress,
        Completed,
        Denied
    }
}
