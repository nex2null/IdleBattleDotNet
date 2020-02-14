using System.Linq;
using System.Collections.Generic;

namespace Framework.Itemization
{
    public class Inventory
    {
        // Properties
        public List<Item> Items { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Inventory()
        {
            Items = new List<Item>();
        }

        /// <summary>
        /// Adds an item to the inventory
        /// </summary>
        public void AddItem(Item item)
        {
            // TODO: Make this whole thing handle stacking better

            // Grab the item information for the item being added
            var itemInformation = ItemData.GetItemInformation(item.Type);

            // Find an item in our list of items that has an amount that can
            // accomodate the amount of the item we are trying to add
            var itemInInventory = Items.FirstOrDefault(x =>
                x.Type == item.Type &&
                x.Amount <= itemInformation.StackSize - item.Amount);

            // If no matching item was found then add the item to the inventory
            // Otherwise just increase the amount of the item we found
            if (itemInInventory != null)
                itemInInventory.Amount += item.Amount;
            else
                Items.Add(item);
        }

        /// <summary>
        /// Adds a list of items to the inventory
        /// </summary>
        public void AddItems(List<Item> items)
        {
            foreach (var item in items)
                AddItem(item);
        }
    }
}