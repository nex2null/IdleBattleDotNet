using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Helpers;
using Framework.Itemization.Enums;

namespace Framework.Itemization.Equipment
{
    public static class EquipmentForge
    {
        /// <summary>
        /// Creates a new piece of equipment
        /// </summary>
        public static Equipment CreateEquipment(
            ItemTypeEnum baseType,
            ItemRarityEnum rarity,
            int itemLevel)
        {
            // Create a normal item of the given base type
            var equipment = new Equipment(
                baseType,
                ItemRarityEnum.Normal,
                itemLevel,
                "Test Equipment",
                GetEquipmentSlot(baseType),
                GenerateImplicits(baseType),    
                new List<EquipmentAffix>(),
                GetEquipmentLevel(baseType)
            );

            // If the rarity is magic or rare then upgrade the item to it
            if (rarity == ItemRarityEnum.Magic || rarity == ItemRarityEnum.Rare)
                UpgradeEquipmentToRarity(equipment, rarity);

            return equipment;
        }

        /// <summary>
        /// Gets the slot for a given base type of equipment
        /// </summary>
        private static EquipmentSlotEnum GetEquipmentSlot(ItemTypeEnum baseType)
        {
            return ItemData.EquipmentInformations.First(x => x.ItemType == baseType).Slot;
        }

        /// <summary>
        /// Gets the level for a given base type of equipment
        /// </summary>
        private static int GetEquipmentLevel(ItemTypeEnum baseType)
        {
            return ItemData.EquipmentInformations.First(x => x.ItemType == baseType).BaseRequiredLevel;
        }

        /// <summary>
        /// Generates all implicits for a piece of equipment
        /// </summary>
        private static List<EquipmentImplicit> GenerateImplicits(ItemTypeEnum baseType)
        {
            // Keep track of generated implicits
            var generatedImplicits = new List<EquipmentImplicit>();

            // Grab all the implicits for the base type
            var baseTypeImplicits = ItemData.EquipmentImplicitInformations.Where(x => x.ItemType == baseType);

            // Generate the implicits
            foreach (var baseImplicit in baseTypeImplicits)
            {
                // Get the implicit value
                var implicitValue = RandomHelper.GetRandomInt(baseImplicit.MinValue, baseImplicit.MaxValue);

                // Create the implicit
                generatedImplicits.Add(new EquipmentImplicit(baseImplicit.ModifiedStat, implicitValue));
            }

            return generatedImplicits;
        }

        private static List<EquipmentAffix> GenerateAffixes(
            EquipmentAffixSlotEnum affixSlot,
            int amountToGenerate,
            int itemLevel)
        {
            // Keep track of generated affixes
            var generatedAffixes = new List<EquipmentAffix>();

            // Get a list of all possible affixes for the affix slot and level
            var filteredAffixes = ItemData.EquipmentAffixInformations.Where(x =>
                x.RequiredLevel <= itemLevel && x.Slot == affixSlot).ToList();

            // Generate the affixes
            for (var i = 0; i < amountToGenerate; i++)
            {
                // Ensure we have affixes to choose from
                if (filteredAffixes.Count == 0)
                    break;

                // Grab a random affix from our list of available ones
                var randomAffix = filteredAffixes[RandomHelper.GetRandomInt(0, filteredAffixes.Count - 1)];

                // Remove all other affixes from our filtered affix list that
                // have the same modified stat as the affix we chose
                filteredAffixes.RemoveAll(x => x.ModifiedStat == randomAffix.ModifiedStat);

                // Roll a random number between the affix min/max
                var affixValue = RandomHelper.GetRandomInt(randomAffix.MinValue, randomAffix.MaxValue);

                // Generate our affix
                generatedAffixes.Add(new EquipmentAffix(randomAffix.Type, affixValue));
            }

            return generatedAffixes;
        }

        /// <summary>
        /// Gets the minimum number of affixes (per slot) for an item rarity
        /// </summary>
        private static int GetMinAffixCountPerSlot(ItemRarityEnum rarity)
        {
            switch (rarity)
            {
                case ItemRarityEnum.Normal: return 0;
                case ItemRarityEnum.Magic: return 1;
                case ItemRarityEnum.Rare: return 1;
                default: throw new Exception($"Could not find max affix count for item rarity: {rarity}");
            }
        }

        /// <summary>
        /// Gets the maximum number of affixes (per slot) for an item rarity
        /// </summary>
        private static int GetMaxAffixCountPerSlot(ItemRarityEnum rarity)
        {
            switch (rarity)
            {
                case ItemRarityEnum.Normal: return 0;
                case ItemRarityEnum.Magic: return 1;
                case ItemRarityEnum.Rare: return 3;
                default: throw new Exception($"Could not find max affix count for item rarity: {rarity}");
            }
        }

        /// <summary>
        /// Resets a piece of equipment back to normal status
        /// </summary>
        public static void ResetEquipmentBackToNormal(Equipment equipment)
        {
            equipment.Affixes.Clear();
            equipment.CraftingTags.Clear();
            equipment.Rarity = ItemRarityEnum.Normal;
        }

        /// <summary>
        /// Upgrades an equipment to a given rarity
        /// </summary>
        public static void UpgradeEquipmentToRarity(Equipment equipment, ItemRarityEnum rarity)
        {
            // Can't upgrade items to normal rarity, downgrade rarity, or set an item to its own rarity
            if (rarity == ItemRarityEnum.Normal ||
                rarity == ItemRarityEnum.Magic && equipment.Rarity == ItemRarityEnum.Rare ||
                rarity == equipment.Rarity)
                return;

            // If the item is a normal item then generate a cool name for it
            // TODO

            // Set the item rarity
            equipment.Rarity = rarity;

            // Populate the affixes
            PopulateEquipmentAffixes(equipment, false);
        }

        /// <summary>
        /// Populates the affixes on a piece of equipment
        /// </summary>
        private static void PopulateEquipmentAffixes(Equipment equipment, bool clearExistingAffixes)
        {
            // Clear existing affixes if we should
            if (clearExistingAffixes)
                equipment.Affixes.Clear();

            // Get existing prefix/suffix counts on the item
            var existingPrefixCount = equipment.CountAffixes(EquipmentAffixSlotEnum.Prefix);
            var existingSuffixCount = equipment.CountAffixes(EquipmentAffixSlotEnum.Suffix);

            // Figure out how many prefixes / suffixes to generate
            var minAffixCount = GetMinAffixCountPerSlot(equipment.Rarity);
            var maxAffixAcount = GetMaxAffixCountPerSlot(equipment.Rarity);
            var prefixCount = RandomHelper.GetRandomInt(minAffixCount - existingPrefixCount, maxAffixAcount - existingPrefixCount);
            var suffixCount = RandomHelper.GetRandomInt(minAffixCount - existingSuffixCount, maxAffixAcount - existingSuffixCount);
            var affixesToGenerate = prefixCount + suffixCount;

            // Make sure we always generate at least 1 new prefix/suffix if we are upgrading and we have room
            var availableAffixRoom = maxAffixAcount * 2 - (existingPrefixCount + existingSuffixCount);
            if (affixesToGenerate == 0 && availableAffixRoom > 0)
                affixesToGenerate = 1;

            // Generate the affixes
            for (var i = 0; i < affixesToGenerate; i++)
                AddRandomAffixToEquipment(equipment);
        }

        // Re-rolls all the affixes on a piece of equipment
        public static void ReRollEquipmentAffixes(Equipment equipment)
        {
            PopulateEquipmentAffixes(equipment, true);
        }

        /// <summary>
        /// Adds a random affix to a piece of equipment
        /// </summary>
        public static void AddRandomAffixToEquipment(Equipment equipment)
        {
            // Get existing prefix/suffix counts on the item
            var existingPrefixCount = equipment.CountAffixes(EquipmentAffixSlotEnum.Prefix);
            var existingSuffixCount = equipment.CountAffixes(EquipmentAffixSlotEnum.Suffix);

            // Sanity check that we are not at the max affixes already
            var maxAffixCountPerSlot = GetMaxAffixCountPerSlot(equipment.Rarity);
            if (existingPrefixCount == maxAffixCountPerSlot && existingSuffixCount == maxAffixCountPerSlot)
                return;

            // We need to determine which affix slot we will generate
            EquipmentAffixSlotEnum slotToGenerate;
            if (existingPrefixCount == maxAffixCountPerSlot)
                slotToGenerate = EquipmentAffixSlotEnum.Suffix;
            else if (existingSuffixCount == maxAffixCountPerSlot)
                slotToGenerate = EquipmentAffixSlotEnum.Prefix;
            else
                slotToGenerate = RandomHelper.CoinFlip() ? EquipmentAffixSlotEnum.Prefix : EquipmentAffixSlotEnum.Suffix;

            // Generate the affix and add to the equipment
            var generatedAffix = GenerateAffixes(slotToGenerate, 1, equipment.ItemLevel);
            if (generatedAffix.Count > 0)
                equipment.Affixes.Add(generatedAffix[0]);
        }

        /// <summary>
        /// Re-roll implicit values on equipment
        /// </summary>
        public static void ReRollEquipmentImplicitValues(Equipment equipment)
        {
            equipment.Implicits = GenerateImplicits(equipment.Type);
        }

        /// <summary>
        /// Re-roll affix values on equipment
        /// </summary>
        public static void ReRollEquipmentAffixValues(Equipment equipment)
        {
            foreach (var affix in equipment.Affixes)
            {
                var affixInformation = ItemData.EquipmentAffixInformations.First(x => x.Type == affix.Type);
                affix.Value = RandomHelper.GetRandomInt(affixInformation.MinValue, affixInformation.MaxValue);
            }
        }
    }
}