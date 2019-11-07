using Framework.Itemization.Enums;

namespace Framework.Itemization.Equipment
{
    public class EquipmentAffix
    {
        // Properties
        public EquipmentAffixTypeEnum Type { get; private set; }
        public int Value { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public EquipmentAffix(EquipmentAffixTypeEnum type, int value)
        {
            Type = type;
            Value = value;
        }
    }
}