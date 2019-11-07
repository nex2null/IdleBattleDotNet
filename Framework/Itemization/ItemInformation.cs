using System.Collections.Generic;
using Framework.Itemization.Enums;

namespace Framework.Itemization
{
    public abstract class ItemInformation
    {
        // Properties
        public ItemTypeEnum ItemType { get; private set; }
        public ItemSuperTypeEnum ItemSuperType { get; private set; }
        public int ItemLevel { get; private set; }
        public int StackSize { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemInformation(
            ItemTypeEnum itemType,
            ItemSuperTypeEnum itemSuperType,
            int itemLevel,
            int stackSize)
        {
            ItemType = itemType;
            ItemSuperType = itemSuperType;
            ItemLevel = itemLevel;
            StackSize = stackSize;
        }
    }
}