using Framework.Itemization.Enums;
using Framework.Itemization;
using System.Collections.Generic;

namespace Framework.Itemization.Equipment
{
    public class Equipment : Item
    {
        // Properties
        public string Name { get; set; }
        public List<EquipmentImplicit> Implicits { get; set; }
        public List<EquipmentAffix> Affixes { get; set; }
        public int RequiredLevel { get; set; }
        public List<EquipmentCraftingTagEnum> CraftingTags { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Equipment(
            ItemTypeEnum type,
            ItemRarityEnum rarity,
            int ilvl,
            string name,
            List<EquipmentImplicit> implicits,
            List<EquipmentAffix> affixes,
            int requiredLevel) : base(type, rarity, ilvl, 1)
        {
            Name = name;
            Implicits = implicits;
            Affixes = affixes;
            RequiredLevel = requiredLevel;
        }
    }
}