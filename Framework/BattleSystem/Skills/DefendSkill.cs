using System.Collections.Generic;
using Framework.BattleSystem.BattleEffects;
using Framework.BattleSystem.Enums;

namespace Framework.BattleSystem.Skills
{
    public class DefendSkill : ISkill
    {
        // Properties
        public string Name => "Defend";
        public TargetTypeEnum TargetType => TargetTypeEnum.Self;

        /// <summary>
        /// Determines if the skill would be beneficial if used on a target
        /// </summary>
        public bool IsBeneficialOn(BattleCharacter target)
        {
            return true;
        }

        /// <summary>
        /// Uses the skill
        /// </summary>
        public void Use(BattleCharacter character, List<BattleCharacter> targets, BattleLog battleLog)
        {
            // Add a defense effect to the character
            var defenseEffect = new DefenseEffect(character);
            character.AddEffect(defenseEffect);

            // Log that the character is defending
            battleLog.AddMessage($"{character.Name} defends");
        }
    }
}
