using System.Collections.Generic;
using Framework.Enums;
using Framework.Itemization.Enums;

namespace Framework.Itemization.Equipment
{
    public class EquipmentImplicitInformation
    {
        // Properties
        public ItemTypeEnum ItemType { get; private set; }
        public StatEnum ModifiedStat { get; private set; }
        public int MinValue { get; private set; }
        public int MaxValue { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public EquipmentImplicitInformation(
            ItemTypeEnum itemType,
            StatEnum modifiedStat,
            int minValue,
            int maxValue)
        {
            ItemType = itemType;
            ModifiedStat = modifiedStat;
            MinValue = minValue;
            MaxValue = maxValue;
        }

        /// <summary>
        /// Builds a list of all equipment implicit informations
        /// </summary>
        public static List<EquipmentImplicitInformation> BuildEquipmentImplicitInformations()
        {
            return new List<EquipmentImplicitInformation>
            {
                new EquipmentImplicitInformation(ItemTypeEnum.RustedChainmail, StatEnum.Hp, 15, 30),
                new EquipmentImplicitInformation(ItemTypeEnum.WornLeatherChest, StatEnum.DodgeChance, 1, 5),
                new EquipmentImplicitInformation(ItemTypeEnum.FrayedClothRobe, StatEnum.Intelligence, 1, 5),
                new EquipmentImplicitInformation(ItemTypeEnum.FrayedClothRobe, StatEnum.Mp, 1, 5)
            };
        }
    }
}