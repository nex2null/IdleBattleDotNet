using System.Linq;
using System;
using System.Collections.Generic;
using Framework.Helpers;
using Framework.Itemization.Enums;
using Framework.Itemization.Equipment;

namespace Framework.Itemization
{
    /// <summary>
    /// An option for generating a piece of loot
    /// </summary>
    public class LootGenerationOption
    {
        // Properties
        public ItemTypeEnum? ItemType { get; private set; }
        public ItemSuperTypeEnum ItemSuperType { get; private set; }
        public ItemRarityEnum? ItemRarity { get; private set; }
        public int ItemLevel { get; private set; }
        public decimal Chance { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public LootGenerationOption(
            ItemTypeEnum? itemType,
            ItemSuperTypeEnum itemSuperType,
            ItemRarityEnum? itemRarity,
            int itemLevel,
            decimal chance)
        {
            ItemType = itemType;
            ItemSuperType = itemSuperType;
            ItemRarity = itemRarity;
            ItemLevel = itemLevel;
            Chance = chance;
        }
    }

    /// <summary>
    /// A currency drop rate
    /// </summary>
    public class CurrencyDropRate
    {
        // Properties        
        public ItemTypeEnum ItemType { get; private set; }
        public int ChanceToDropPerThousand { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public CurrencyDropRate(ItemTypeEnum itemType, int chanceToDropPerThousand)
        {
            this.ItemType = itemType;
            this.ChanceToDropPerThousand = chanceToDropPerThousand;
        }
    }

    /// <summary>
    /// A single row in the currency drop table
    /// </summary>
    public class CurrencyDropTableRow
    {
        public ItemTypeEnum ItemType { get; set; }
        public int MinNumber { get; set; }
        public int MaxNumber { get; set; }
    }

    /// <summary>
    /// Drops some currency
    /// </summary>
    public class CurrencyDropper
    {
        // Properties
        public List<CurrencyDropTableRow> DropTable { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public CurrencyDropper(List<CurrencyDropRate> currencyDropRates)
        {
            InitializeDropTable(currencyDropRates);
        }

        /// <summary>
        /// Initializes the drop table
        /// </summary>
        public void InitializeDropTable(List<CurrencyDropRate> currencyDropRates)
        {
            // Keep track of the sum 'chance to drop per thousand' of the drop rates
            var chanceToDropPerThousandSum = 0;

            foreach (var dropRate in currencyDropRates)
            {
                DropTable.Add(new CurrencyDropTableRow
                {
                    ItemType = dropRate.ItemType,
                    MinNumber = chanceToDropPerThousandSum + 1,
                    MaxNumber = chanceToDropPerThousandSum + dropRate.ChanceToDropPerThousand
                });

                chanceToDropPerThousandSum += dropRate.ChanceToDropPerThousand;
            }

            // Verify that the sum chance to drop of all currency items' drop rates equals 1000
            if (chanceToDropPerThousandSum != 1000)
                throw new Exception("Total drop rates did not equal 1000");
        }

        /// <summary>
        /// Gets a random piece of currency
        /// </summary>
        public ItemTypeEnum GetRandomCurrency(int itemLevel)
        {
            // TODO: Do something with ilvl?

            // Choose a random number between 1 and 1000
            var randomNumber = RandomHelper.GetRandomInt(1, 1000);

            // Find the currency item in the drop table matching that random number
            foreach (var row in DropTable)
            {
                if (randomNumber >= row.MinNumber && randomNumber <= row.MaxNumber)
                    return row.ItemType;
            }

            throw new Exception("Item type could not be found");
        }
    }

    /// <summary>
    /// Generates loot
    /// </summary>
    public static class LootGenerator
    {
        // Properties
        private static CurrencyDropper _currencyDropper;

        /// <summary>
        /// Constructor
        /// </summary>
        static LootGenerator()
        {
            // Initialize currency dropper
            _currencyDropper = new CurrencyDropper(new List<CurrencyDropRate>
            {
                new CurrencyDropRate(ItemTypeEnum.OrbOfAbolition, 40),
                new CurrencyDropRate(ItemTypeEnum.OrbOfImbuing, 260),
                new CurrencyDropRate(ItemTypeEnum.OrbOfConjury, 100),
                new CurrencyDropRate(ItemTypeEnum.OrbOfPromotion, 90),
                new CurrencyDropRate(ItemTypeEnum.OrbOfMutation, 260),
                new CurrencyDropRate(ItemTypeEnum.OrbOfPandemonium, 50),
                new CurrencyDropRate(ItemTypeEnum.OrbOfThaumaturgy, 70),
                new CurrencyDropRate(ItemTypeEnum.OrbOfFortune, 10),
                new CurrencyDropRate(ItemTypeEnum.OrbOfBalance, 50),
                new CurrencyDropRate(ItemTypeEnum.OrbOfPerfection, 30),
                new CurrencyDropRate(ItemTypeEnum.AlphaOrb, 2),
                new CurrencyDropRate(ItemTypeEnum.OmegaOrb, 2),
                new CurrencyDropRate(ItemTypeEnum.OrbOfExtraction, 4),
                new CurrencyDropRate(ItemTypeEnum.OrbOfDistillation, 2),
                new CurrencyDropRate(ItemTypeEnum.OrbOfEmpowerment, 30)
            });
        }

        /// <summary>
        /// Generates loot
        /// </summary>
        public static List<Item> GenerateLoot(
            int maxItemsToGenerate,
            List<LootGenerationOption> options)
        {
            // Copy the generation options
            var availableOptions = options.ToList();

            // Determine how many items to generate
            var itemsToGenerate = RandomHelper.GetRandomInt(0, maxItemsToGenerate);

            // Keep track of generated items
            var generatedItems = new List<Item>();

            // Keep track of the loot generation option we last generated an item from
            LootGenerationOption lastUsedOption = null;

            // Generate the items        
            for (var i = 0; i < itemsToGenerate; i++)
            {
                // Loop through the available loot generation options until we generate an item
                // or we run have enumerated all the options
                for (var j = 0; j < availableOptions.Count; j++)
                {
                    var item = GenerateSingleLoot(availableOptions[j]);
                    if (item != null)
                    {
                        generatedItems.Add(item);
                        lastUsedOption = availableOptions[j];
                        break;
                    }
                }

                // If we used an option from the list then ensure we don't use it again
                if (lastUsedOption != null)
                {
                    availableOptions.RemoveAll(x => x == lastUsedOption);
                    lastUsedOption = null;
                }
            }

            return generatedItems;
        }

        /// <summary>
        /// Generates a single piece of loot (or nothing) given an option
        /// </summary>
        private static Item GenerateSingleLoot(LootGenerationOption option)
        {
            // Determine if we are going to generate anything
            if (RandomHelper.GetRandomInt(1, 100) > option.Chance)
                return null;

            // Determine the item type
            var itemType = option.ItemType ?? GetRandomItemType(option.ItemSuperType);

            // Determine the item rarity
            var itemRarity = option.ItemRarity ?? GetRandomItemRarity(option.ItemSuperType);

            // Determine the item level
            var itemLevel = option.ItemSuperType == ItemSuperTypeEnum.Currency ? 1 : option.ItemLevel;

            // If the item super type is an equipment then forge the equipment
            if (option.ItemSuperType == ItemSuperTypeEnum.Equipment)
                return EquipmentForge.CreateEquipment(itemType, itemRarity, itemLevel);

            // If the item super type is a currency then drop some random currency
            if (option.ItemSuperType == ItemSuperTypeEnum.Currency)
                return GetRandomCurrencyItem(itemLevel);

            throw new Exception("Should never get here");
        }

        /// <summary>
        /// Gets a random item type given a super type
        /// </summary>
        private static ItemTypeEnum GetRandomItemType(ItemSuperTypeEnum superType)
        {
            // TODO: Make this not completely random, obviously
            var validItems = ItemData.AllItemInformations.Where(x => x.ItemSuperType == superType);
            return validItems.ElementAt(RandomHelper.GetRandomInt(0, validItems.Count())).ItemType;
        }

        /// <summary>
        /// Gets a random item rarity given a super type
        /// </summary>
        private static ItemRarityEnum GetRandomItemRarity(ItemSuperTypeEnum superType)
        {
            // TODO: Make this make more sense, obviously

            // Currency items are always normal
            if (superType == ItemSuperTypeEnum.Currency)
                return ItemRarityEnum.Normal;

            // Figure out the item rarity
            var randomNumber = RandomHelper.GetRandomInt(1, 100);

            // Return a random rarity
            if (randomNumber <= 50)
                return ItemRarityEnum.Normal;
            else if (randomNumber <= 85)
                return ItemRarityEnum.Magic;
            else
                return ItemRarityEnum.Rare;
        }

        /// <summary>
        /// Gets a random currency item
        /// </summary>
        private static Item GetRandomCurrencyItem(int itemLevel)
        {
            var itemType = _currencyDropper.GetRandomCurrency(itemLevel);
            return new Item(itemType, ItemRarityEnum.Normal, 1, 1);
        }
    }
}