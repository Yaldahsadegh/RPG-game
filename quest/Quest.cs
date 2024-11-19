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
        public string Status { get; set; } // e.g., "Not Started", "In Progress", "Completed"
        public NPC AssignedNPC { get; set; }
        public bool IsAccepted { get; set; } // Track if the quest is accepted

        public Quest(string title, string description, NPC assignedNPC)
        {
            Title = title;
            Description = description;
            AssignedNPC = assignedNPC;
            Status = "Not Started"; // Default status
            IsAccepted = false; // Default to not accepted
        }
    }
}
