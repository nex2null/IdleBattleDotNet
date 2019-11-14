using System.Collections.Generic;
using System.Linq;
using Framework.Itemization.Enums;
using Framework.Itemization.Equipment;

namespace Framework.Itemization
{
    public static class ItemData
    {
        // Properties
        private static List<CurrencyInformation> CurrencyInformations { get; set; }
        private static List<EquipmentInformation> EquipmentInformations { get; set; }
        private static List<EquipmentImplicitInformation> EquipmentImplicitInformations { get; set; }
        public static List<EquipmentAffixInformation> EquipmentAffixInformations { get; set; }
        private static List<ItemInformation> AllItemInformations { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        static ItemData()
        {
            CurrencyInformations = CurrencyInformation.BuildCurrencyInformations();
            EquipmentInformations = EquipmentInformation.BuildEquipmentInformations();
            EquipmentImplicitInformations = EquipmentImplicitInformation.BuildEquipmentImplicitInformations();
            EquipmentAffixInformations = EquipmentAffixInformation.BuildEquipmentAffixInformations();

            AllItemInformations = new List<ItemInformation>();
            AllItemInformations.AddRange(EquipmentInformations);
            AllItemInformations.AddRange(CurrencyInformations);
        }

        /// <summary>
        /// Gets the affix information for a given affix type
        /// </summary>
        public static EquipmentAffixInformation GetAffixInformation(EquipmentAffixTypeEnum affixType)
        {
            return EquipmentAffixInformations.First(x => x.Type == affixType);
        }

        /// <summary>
        /// Gets the item information for a given item type
        /// </summary>
        public static ItemInformation GetItemInformation(ItemTypeEnum itemType)
        {
            return AllItemInformations.First(x => x.ItemType == itemType);
        }

        /// <summary>
        /// Gets the currency information for a given item type
        /// </summary>
        public static CurrencyInformation GetCurrencyInformation(ItemTypeEnum itemType)
        {
            return CurrencyInformations.First(x => x.ItemType == itemType);
        }

        /// <summary>
        /// Gets the equipment information for a given item type
        /// </summary>
        public static EquipmentInformation GetEquipmentInformation(ItemTypeEnum itemType)
        {
            return EquipmentInformations.First(x => x.ItemType == itemType);
        }
        
        /// <summary>
        /// Gets the equipment implicit information for a given item type
        /// </summary>
        public static List<EquipmentImplicitInformation> GetEquipmentImplicitInformation(ItemTypeEnum itemType)
        {
            return EquipmentImplicitInformations.Where(x => x.ItemType == itemType).ToList();
        }

        /// <summary>
        /// Gets all items of a given super type
        /// </summary>
        public static List<ItemInformation> GetAllItemsOfSuperType(ItemSuperTypeEnum superType)
        {
            return AllItemInformations.Where(x => x.ItemSuperType == superType).ToList();
        }
    }
}