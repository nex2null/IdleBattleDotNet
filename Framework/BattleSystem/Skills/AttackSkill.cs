using System.Collections.Generic;
using Framework.BattleSystem.Enums;

namespace Framework.BattleSystem.Skills
{
    public class AttackSkill : ISkill
    {
        // Properties
        public string Name => "Attack";
        public TargetTypeEnum TargetType => TargetTypeEnum.Single;

        /// <summary>
        /// Determines if the skill would be beneficial if used on a target
        /// </summary>
        public bool IsBeneficialOn(BattleCharacter target)
        {
            return target.IsAlive();
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

            // If the attack hits calculate damage
            if (attackHits)
            {
                // Calculate the damage on the target
                var damageToDo = CalculateDamage(character, target);

                // Apply the damage
                target.ApplyDamage(damageToDo);

                // Log
                battleLog.AddMessage($"{character.Name} attacks {target.Name} for {damageToDo.Amount} damage");
                if (!target.IsAlive())
                    battleLog.AddMessage($"{target.Name} has died");
            }
            else
            {
                battleLog.AddMessage($"{character.Name} attacks {target.Name}, but misses!");
            }
        }

        /// <summary>
        /// Calculates the attack damage
        /// </summary>
        public BattleDamage CalculateDamage(BattleCharacter user, BattleCharacter target)
        {
            // Base damage is just the strength of the attacker * 5
            var baseDamageAmount = user.Str * 5;
            var baseDamage = new BattleDamage(baseDamageAmount, DamageTypeEnum.Physical);

            // Process the base damage
            return BattleFormulas.ProcessDamage(user, target, baseDamage);
        }
    }
}
