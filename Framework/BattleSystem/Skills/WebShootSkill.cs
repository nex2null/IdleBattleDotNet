using System.Collections.Generic;
using Framework.BattleSystem.BattleEffects;
using Framework.BattleSystem.Enums;

namespace Framework.BattleSystem.Skills
{
    public class WebShootSkill : ISkill
    {
        // Properties
        public string Name => "Web Shoot";
        public TargetTypeEnum TargetType => TargetTypeEnum.Single;

        /// <summary>
        /// Determines if the skill would be beneficial if used on a target
        /// </summary>
        public bool IsBeneficialOn(BattleCharacter target)
        {
            return target.GetEffect("Slowed") == null;
        }

        /// <summary>
        /// Uses the skill
        /// </summary>
        public void Use(BattleCharacter character, List<BattleCharacter> targets, BattleLog battleLog)
        {
            // Only first target is ever relevant
            var target = targets[0];

            // Determine if the attack hits
            var attackHits = BattleFormulas.CalculateHit(character, target);

            // If the attack hits then apply the slow
            if (attackHits)
            {
                var slowedEffect = new SlowedEffect(target, 3);
                target.AddEffect(slowedEffect);
                battleLog.AddMessage($"{character.Name} shoots a sticky web at {target.Name}, they are slowed!");
            }
            else
            {
                battleLog.AddMessage($"{character.Name} shoots a sticky web at {target.Name}, but misses!");
            }
        }
    }
}