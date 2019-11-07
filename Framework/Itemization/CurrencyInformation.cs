using System;
using System.Collections.Generic;
using Framework.Itemization.Enums;

namespace Framework.Itemization
{
    public class CurrencyInformation : ItemInformation
    {
        // Properties
        public bool IsTargeted { get; private set; }
        public ItemSuperTypeEnum? TargetItemSuperType { get; private set; }
        public Func<Item, bool> Use { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public CurrencyInformation(
            ItemTypeEnum itemType,
            bool isTargeted,
            ItemSuperTypeEnum? targetItemSuperType,
            Func<Item, bool> use) : base(itemType, ItemSuperTypeEnum.Currency, 1, 99)
        {
            IsTargeted = isTargeted;
            TargetItemSuperType = targetItemSuperType;
            Use = use;
        }

        /// <summary>
        /// Uses an orb of abolition on an item
        /// </summary>
        public static bool UseOrbOfAbolition(Item item)
        {
            // Verify the item is an equipment
            var equipment = item as Equipment.Equipment;
            if (equipment == null)
                return false;

            // Verify the equipment is not normal
            if (equipment.Rarity == ItemRarityEnum.Normal)
                return false;

            // Reset the equipment back to normal
            return true;
        }

        public static List<CurrencyInformation> BuildCurrencyInformations()
        {
            return new List<CurrencyInformation>
            {
                // Orb of abolition
                new CurrencyInformation(
                    ItemTypeEnum.OrbOfAbolition,
                    true,
                    ItemSuperTypeEnum.Equipment,
                    item => UseOrbOfAbolition(item)
                )
            };
        }
    }
}