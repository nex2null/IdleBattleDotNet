using Framework.BattleSystem.Enums;

namespace Framework.BattleSystem.BattleEffects
{
    public class DefenseEffect : BaseEffect
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DefenseEffect(BattleCharacter character)
            : base(character, "Defended")
        {
        }

        /// <summary>
        /// Handles logic for before a character action is performed
        /// </summary>
        public override void BeforeActionPerformed()
        {
            Character.RemoveEffect(this);
        }

        /// <summary>
        /// Handles logic for before damage is taken
        /// </summary>
        public override void BeforeDamageTaken(BattleDamage damage)
        {
            // Physical damage taken is halved while this effect is applied
            if (damage.Type == DamageTypeEnum.Physical)
                damage.Amount = damage.Amount * .5M;
        }
    }
}
