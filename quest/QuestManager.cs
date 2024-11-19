using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.quest
{
    public class QuestManager : ISubject
    {
        private static QuestManager instance;
        private List<IObserver> observers = new List<IObserver>();
        private List<Quest> quests = new List<Quest>();

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

        public void StartQuest(string title, string description, NPC assignedNPC)
        {
            var quest = new Quest(title, description, assignedNPC);
            quests.Add(quest);
            Notify($"Quest Available: {quest.Title} - {quest.Description}");
        }

        public void AcceptQuest(string title)
        {
            var quest = quests.FirstOrDefault(q => q.Title == title);
            if (quest != null && !quest.IsAccepted)
            {
                quest.IsAccepted = true;
                quest.Status = "In Progress";
                Notify($"Quest Accepted: {quest.Title}");
            }
        }

        public void DenyQuest(string title)
        {
            var quest = quests.FirstOrDefault(q => q.Title == title);
            if (quest != null)
            {
                Notify($"Quest Denied: {quest.Title}");
            }
        }

        public void CompleteQuest(string title)
        {
            var quest = quests.FirstOrDefault(q => q.Title == title);
            if (quest != null && quest.IsAccepted)
            {
                quest.Status = "Completed";
                Notify($"Quest Completed: {quest.Title}");
            }
        }

        public List<Quest> GetQuests()
        {
            return quests; // Return the list of quests for viewing
        }
    }
}
