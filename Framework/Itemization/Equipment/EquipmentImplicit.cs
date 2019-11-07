using Framework.Enums;

namespace Framework.Itemization.Equipment
{
    public class EquipmentImplicit
    {
        // Properties
        public StatEnum Stat { get; private set; }
        public int Value { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public EquipmentImplicit(StatEnum stat, int value)
        {
            Stat = stat;
            Value = value;
        }
    }
}