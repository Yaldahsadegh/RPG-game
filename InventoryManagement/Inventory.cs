using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.InventoryManagement
{
    public class Inventory
    {
        private List<Item> items = new List<Item>();

        public void AddItemToInventory(Item item)
        {
            items.Add(item);
        }

        public void RemoveItemFromInventory(Item item)
        {
            items.Remove(item);
        }

        public void ListItems()
        {
            if (items.Count == 0)
            {
                Console.WriteLine("No items in the inventory.");
                return;
            }

            Console.WriteLine("Inventory Items:");
            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {items[i].Name} (Rarity: {items[i].Rarity})");
            }
        }

        public Item GetItemByName(string name)
        {
            return items.FirstOrDefault(item => item.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public bool ItemExists(string itemName)
        {
            return items.Any(item => item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        }
    }
}

