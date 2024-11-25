using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.quest
{
    public class Quest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public QuestStatus Status { get; set; } // Quest status (NotAccepted, InProgress, Completed, Denied)
        public NPC AssignedNPC { get; set; }
        public string EnemyName { get; set; } // The enemy that needs to be defeated in the quest

        public Quest(string title, string description, NPC assignedNPC, string enemyName)
        {
            Title = title;
            Description = description;
            AssignedNPC = assignedNPC;
            EnemyName = enemyName;
            Status = QuestStatus.NotAccepted; // Default status
        }
    }
}
