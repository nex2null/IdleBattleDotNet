using Framework.Itemization.Enums;

namespace Framework.Itemization
{
    public class Item
    {
        // Properties
        public ItemTypeEnum Type { get; set; }
        public ItemRarityEnum Rarity { get; set; }
        public int ItemLevel { get; set; }
        public int Amount { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Item(
            ItemTypeEnum type,
            ItemRarityEnum rarity,
            int itemLevel,
            int amount)
        {
            Type = type;
            Rarity = rarity;
            ItemLevel = itemLevel;
            Amount = amount;
        }
    }
}