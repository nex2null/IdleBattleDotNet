using System.Collections.Generic;
using Framework.Itemization.Enums;

namespace Framework.Itemization.Equipment
{
    public class EquipmentInformation : ItemInformation
    {
        // Properties
        public EquipmentSlotEnum Slot { get; private set; }
        public int BaseRequiredLevel { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public EquipmentInformation(
            ItemTypeEnum itemType,
            EquipmentSlotEnum slot,
            int baseRequiredLevel) : base(itemType, ItemSuperTypeEnum.Equipment, 1, 1)
        {
            Slot = slot;
            BaseRequiredLevel = baseRequiredLevel;
        }

        /// <summary>
        /// Builds the list of item informations
        /// </summary>
        public static List<EquipmentInformation> BuildEquipmentInformations()
        {
            return new List<EquipmentInformation>
            {
                new EquipmentInformation(ItemTypeEnum.FrayedClothRobe, EquipmentSlotEnum.ChestPiece, 1),
                new EquipmentInformation(ItemTypeEnum.RustedChainmail, EquipmentSlotEnum.ChestPiece, 1),
                new EquipmentInformation(ItemTypeEnum.WornLeatherChest, EquipmentSlotEnum.ChestPiece, 1)
            };
        }
    }
}