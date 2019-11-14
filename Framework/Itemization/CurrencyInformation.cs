using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Itemization.Enums;
using Framework.Itemization.Equipment;

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
            EquipmentForge.ResetEquipmentBackToNormal(equipment);
            return true;
        }

        /// <summary>
        /// Uses an orb of imbuing on an item
        /// </summary>
        public static bool UseOrbOfImbuing(Item item)
        {
            // Verify the item is an equipment
            var equipment = item as Equipment.Equipment;
            if (equipment == null)
                return false;

            // Verify the equipment is normal
            if (equipment.Rarity != ItemRarityEnum.Normal)
                return false;

            // Upgrade the equipment to magic
            EquipmentForge.UpgradeEquipmentToRarity(equipment, ItemRarityEnum.Magic);
            return true;
        }

        /// <summary>
        /// Uses an orb of conjury on an item
        /// </summary>
        public static bool UseOrbOfConjury(Item item)
        {
            // Verify the item is an equipment
            var equipment = item as Equipment.Equipment;
            if (equipment == null)
                return false;

            // Verify the equipment is normal
            if (equipment.Rarity != ItemRarityEnum.Normal)
                return false;

            // Upgrade the equipment to rare
            EquipmentForge.UpgradeEquipmentToRarity(equipment, ItemRarityEnum.Rare);
            return true;
        }

        /// <summary>
        /// Uses an orb of promotion on an item
        /// </summary>
        public static bool UseOrbOfPromotion(Item item)
        {
            // Verify the item is an equipment
            var equipment = item as Equipment.Equipment;
            if (equipment == null)
                return false;

            // Verify the equipment is magic
            if (equipment.Rarity != ItemRarityEnum.Magic)
                return false;

            // Upgrade the equipment to rare
            EquipmentForge.UpgradeEquipmentToRarity(equipment, ItemRarityEnum.Rare);
            return true;
        }

        /// <summary>
        /// Uses an orb of mutation on an item
        /// </summary>
        public static bool UseOrbOfMutation(Item item)
        {
            // Verify the item is an equipment
            var equipment = item as Equipment.Equipment;
            if (equipment == null)
                return false;

            // Verify the equipment is magic
            if (equipment.Rarity != ItemRarityEnum.Magic)
                return false;

            // Re-roll the equipment affixes
            EquipmentForge.ReRollEquipmentAffixes(equipment);
            return true;
        }

        /// <summary>
        /// Uses an orb of pandemonium on an item
        /// </summary>
        public static bool UseOrbOfPandemonium(Item item)
        {
            // Verify the item is an equipment
            var equipment = item as Equipment.Equipment;
            if (equipment == null)
                return false;

            // Verify the equipment is rare
            if (equipment.Rarity != ItemRarityEnum.Rare)
                return false;

            // Re-roll the equipment affixes
            EquipmentForge.ReRollEquipmentAffixes(equipment);
            return true;
        }

        /// <summary>
        /// Uses an orb of thaumaturgy on an item
        /// </summary>
        public static bool UseOrbOfThaumaturgy(Item item)
        {
            // Verify the item is an equipment
            var equipment = item as Equipment.Equipment;
            if (equipment == null)
                return false;

            // Verify the equipment is magic
            if (equipment.Rarity != ItemRarityEnum.Magic)
                return false;

            // Grab the existing affix count
            var existingAffixCount = equipment.Affixes.Count;

            // Attempt to add an affix to the equipment
            EquipmentForge.AddRandomAffixToEquipment(equipment);

            // If an affix was added then the item was successful, otherwise it was not
            return existingAffixCount < equipment.Affixes.Count;
        }

        /// <summary>
        /// Uses an orb of fortune on an item
        /// </summary>
        public static bool UseOrbOfFortune(Item item)
        {
            // Verify the item is an equipment
            var equipment = item as Equipment.Equipment;
            if (equipment == null)
                return false;

            // Verify the equipment is rare
            if (equipment.Rarity != ItemRarityEnum.Rare)
                return false;

            // Grab the existing affix count
            var existingAffixCount = equipment.Affixes.Count;

            // Attempt to add an affix to the equipment
            EquipmentForge.AddRandomAffixToEquipment(equipment);

            // If an affix was added then the item was successful, otherwise it was not
            return existingAffixCount < equipment.Affixes.Count;
        }

        /// <summary>
        /// Uses an orb of balance on an item
        /// </summary>
        public static bool UseOrbOfBalance(Item item)
        {
            // Verify the item is an equipment
            var equipment = item as Equipment.Equipment;
            if (equipment == null)
                return false;

            // Re-roll the implicit values
            EquipmentForge.ReRollEquipmentImplicitValues(equipment);
            return true;
        }

        /// <summary>
        /// Uses an orb of perfection on an item
        /// </summary>
        public static bool UseOrbOfPerfection(Item item)
        {
            // Verify the item is an equipment
            var equipment = item as Equipment.Equipment;
            if (equipment == null)
                return false;

            // Verify the equipment has affixes
            if (equipment.Affixes.Count == 0)
                return false;

            // Re-roll the affix values
            EquipmentForge.ReRollEquipmentAffixValues(equipment);
            return true;
        }

        /// <summary>
        /// Uses an alpha orb on an item
        /// </summary>
        public static bool UseAlphaOrb(Item item)
        {
            // Verify the item is an equipment
            var equipment = item as Equipment.Equipment;
            if (equipment == null)
                return false;

            // Verify the equipment does not have frozen prefixes
            if (equipment.CraftingTags.Contains(EquipmentCraftingTagEnum.PrefixesCannotBeChanged))
                return false;

            // Add the prefixes cannot be changed crafting tag
            equipment.CraftingTags.Add(EquipmentCraftingTagEnum.PrefixesCannotBeChanged);
            return true;
        }

        /// <summary>
        /// Uses an omega orb on an item
        /// </summary>
        public static bool UseOmegaOrb(Item item)
        {
            // Verify the item is an equipment
            var equipment = item as Equipment.Equipment;
            if (equipment == null)
                return false;

            // Verify the equipment does not have frozen suffixes
            if (equipment.CraftingTags.Contains(EquipmentCraftingTagEnum.SuffixesCannotBeChanged))
                return false;

            // Add the suffixes cannot be changed crafting tag
            equipment.CraftingTags.Add(EquipmentCraftingTagEnum.SuffixesCannotBeChanged);
            return true;
        }

        /// <summary>
        /// Uses an orb of extraction on an item
        /// </summary>
        public static bool UseOrbOfExtraction(Item item)
        {
            // TODO - Extract affix from a magical item
            return false;
        }

        /// <summary>
        /// Uses an orb of distillation on an item
        /// </summary>
        public static bool UseOrbOfDistillation(Item item)
        {
            // TODO - Extract affix from a rare item
            return false;
        }

        /// <summary>
        /// Uses an orb of empowerment
        /// </summary>
        public static bool UseOrbOfEmpowerment()
        {
            // TODO
            return false;
        }

        /// <summary>
        /// Builds up the list of currency informations
        /// </summary>
        public static List<CurrencyInformation> BuildCurrencyInformations()
        {
            return new List<CurrencyInformation>
            {
                // Orb of abolition
                new CurrencyInformation(
                    ItemTypeEnum.OrbOfAbolition,
                    true,
                    ItemSuperTypeEnum.Equipment,
                    UseOrbOfAbolition
                ),

                // Orb of imbuing
                new CurrencyInformation(
                    ItemTypeEnum.OrbOfImbuing,
                    true,
                    ItemSuperTypeEnum.Equipment,
                    UseOrbOfImbuing
                ),

                // Orb of conjury
                new CurrencyInformation(
                    ItemTypeEnum.OrbOfConjury,
                    true,
                    ItemSuperTypeEnum.Equipment,
                    UseOrbOfConjury
                ),

                // Orb of promotion
                new CurrencyInformation(
                    ItemTypeEnum.OrbOfPromotion,
                    true,
                    ItemSuperTypeEnum.Equipment,
                    UseOrbOfPromotion
                ),

                // Orb of mutation
                new CurrencyInformation(
                    ItemTypeEnum.OrbOfMutation,
                    true,
                    ItemSuperTypeEnum.Equipment,
                    UseOrbOfMutation
                ),

                // Orb of pandemonium
                new CurrencyInformation(
                    ItemTypeEnum.OrbOfPandemonium,
                    true,
                    ItemSuperTypeEnum.Equipment,
                    UseOrbOfPandemonium
                ),

                // Orb of thaumaturgy
                new CurrencyInformation(
                    ItemTypeEnum.OrbOfThaumaturgy,
                    true,
                    ItemSuperTypeEnum.Equipment,
                    UseOrbOfThaumaturgy
                ),

                // Orb of fortune
                new CurrencyInformation(
                    ItemTypeEnum.OrbOfFortune,
                    true,
                    ItemSuperTypeEnum.Equipment,
                    UseOrbOfFortune
                ),

                // Orb of balance
                new CurrencyInformation(
                    ItemTypeEnum.OrbOfBalance,
                    true,
                    ItemSuperTypeEnum.Equipment,
                    UseOrbOfBalance
                ),

                // Orb of perfection
                new CurrencyInformation(
                    ItemTypeEnum.OrbOfPerfection,
                    true,
                    ItemSuperTypeEnum.Equipment,
                    UseOrbOfPerfection
                ),

                // Alpha orb
                new CurrencyInformation(
                    ItemTypeEnum.AlphaOrb,
                    true,
                    ItemSuperTypeEnum.Equipment,
                    UseAlphaOrb
                ),

                // Omega orb
                new CurrencyInformation(
                    ItemTypeEnum.OmegaOrb,
                    true,
                    ItemSuperTypeEnum.Equipment,
                    UseOmegaOrb
                ),

                // Orb of extraction
                new CurrencyInformation(
                    ItemTypeEnum.OrbOfExtraction,
                    true,
                    ItemSuperTypeEnum.Equipment,
                    UseOrbOfExtraction
                ),

                // Orb of distillation
                new CurrencyInformation(
                    ItemTypeEnum.OrbOfDistillation,
                    true,
                    ItemSuperTypeEnum.Equipment,
                    UseOrbOfDistillation
                ),

                // Orb of empowerment
                new CurrencyInformation(
                    ItemTypeEnum.OrbOfEmpowerment,
                    false,
                    null,
                    item => UseOrbOfEmpowerment()
                ),
            };
        }
    }
}