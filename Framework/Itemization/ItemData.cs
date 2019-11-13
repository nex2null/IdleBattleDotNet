using System.Collections.Generic;
using System.Linq;
using Framework.Itemization.Enums;
using Framework.Itemization.Equipment;

namespace Framework.Itemization
{
    public static class ItemData
    {
        // Properties
        public static List<EquipmentInformation> EquipmentInformations { get; private set; }
        public static List<EquipmentImplicitInformation> EquipmentImplicitInformations { get; private set; }
        public static List<EquipmentAffixInformation> EquipmentAffixInformations { get; private set; }
        public static List<ItemInformation> AllItemInformations { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        static ItemData()
        {
            EquipmentInformations = EquipmentInformation.BuildEquipmentInformations();
            EquipmentImplicitInformations = EquipmentImplicitInformation.BuildEquipmentImplicitInformations();
            EquipmentAffixInformations = EquipmentAffixInformation.BuildEquipmentAffixInformations();

            AllItemInformations = new List<ItemInformation>();
            AllItemInformations.AddRange(EquipmentInformations);
        }

        /// <summary>
        /// Gets the affix information for a given affix type
        /// </summary>
        public static EquipmentAffixInformation GetAffixInformation(EquipmentAffixTypeEnum affixType)
        {
            return EquipmentAffixInformations.First(x => x.Type == affixType);
        }
    }
}