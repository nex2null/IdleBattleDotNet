using Framework.BattleSystem.Enums;

namespace Framework.BattleSystem
{
    public class BattleDamage
    {
        // Properties
        public decimal Amount { get; set; }
        public DamageTypeEnum Type { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public BattleDamage(decimal amount, DamageTypeEnum type)
        {
            Amount = amount;
            Type = type;
        }
    }
}
