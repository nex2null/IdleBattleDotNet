using Framework.Itemization.Enums;
using Framework.Itemization;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Itemization.Equipment
{
    public class Equipment : Item
    {
        // Properties
        public string Name { get; set; }
        public List<EquipmentImplicit> Implicits { get; set; }
        public List<EquipmentAffix> Affixes { get; set; }
        public int RequiredLevel { get; set; }
        public EquipmentSlotEnum Slot { get; set; }
        public List<EquipmentCraftingTagEnum> CraftingTags { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Equipment(
            ItemTypeEnum type,
            ItemRarityEnum rarity,
            int ilvl,
            string name,
            EquipmentSlotEnum slot,
            List<EquipmentImplicit> implicits,
            List<EquipmentAffix> affixes,
            int requiredLevel) : base(type, rarity, ilvl, 1)
        {
            Name = name;
            Slot = slot;
            Implicits = implicits;
            Affixes = affixes;
            RequiredLevel = requiredLevel;
        }

        /// <summary>
        /// Counts the affixes of a given slot on the equipment
        /// </summary>
        public int CountAffixes(EquipmentAffixSlotEnum affixSlot)
        {
            return Affixes.Count(x => ItemData.GetAffixInformation(x.Type).Slot == affixSlot);
        }
    }
}