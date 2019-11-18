using System.Collections.Generic;
using Framework.BattleSystem.Enums;

namespace Framework.BattleSystem.Skills
{
    public interface ISkill
    {
        // Properties
        string Name { get; }
        TargetTypeEnum TargetType { get; }

        /// <summary>
        /// Determines if the skill would be beneficial if used on a target
        /// </summary>
        bool IsBeneficialOn(BattleCharacter target);

        /// <summary>
        /// Uses the skill
        /// </summary>
        void Use(BattleCharacter character, List<BattleCharacter> targets, BattleLog battleLog);
    }
}
