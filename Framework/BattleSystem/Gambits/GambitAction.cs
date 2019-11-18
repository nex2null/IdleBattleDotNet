using System;
using System.Collections.Generic;
using System.Linq;
using Framework.BattleSystem.Enums;
using Framework.BattleSystem.Gambits.Conditions;
using Framework.BattleSystem.Skills;
using Framework.Helpers;

namespace Framework.BattleSystem.Gambits
{
    public class GambitAction
    {
        // Properties
        public IGambitCondition Condition { get; set; }
        public string ConditionInput { get; set; }
        public GambitTypeEnum Type { get; set; }
        public ISkill Action { get; set; }
        public decimal ActivationChance { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public GambitAction(
            IGambitCondition condition,
            string conditionInput,
            GambitTypeEnum type,
            string actionName,
            decimal? activationChance = null)
        {
            Condition = condition;
            ConditionInput = conditionInput;
            Type = type;
            Action = FindAction(actionName);
            ActivationChance = activationChance ?? 1;
        }

        /// <summary>
        /// Get the action to perform
        /// </summary>
        public GambitActionResult GetAction(BattleCharacter user, List<BattleCharacter> characters)
        {
            // TODO: Don't make this dynamic

            // Determine if we can activate
            if (ActivationChance < 1 && RandomHelper.GetRandomDecimal(0, 1) > ActivationChance)
                return null;

            // Grab the potential matches for the condition
            var potentialTargets = Condition.GetTargets(user, characters);

            // Filter the targets by which ones the action can be beneficial on
            var targets = potentialTargets.Where(x => Action.IsBeneficialOn(x));

            // If this is a skill action, and the skill target is 'self' then
            // only allow the target to be the caster
            if (Type == GambitTypeEnum.Skill && Action.TargetType == TargetTypeEnum.Self)
                targets = targets.Where(x => x == user);

            // If there are no targets left then the action cannot be performed
            if (targets.Count() == 0)
                return null;

            // Return the action
            return new GambitActionResult
            {
                Skill = Action,
                Targets = targets.ToList(),
                Type = Type
            };
        }

        /// <summary>
        /// Find the action to perform based on the name
        /// </summary>
        private ISkill FindAction(string actionName)
        {
            switch (actionName.ToUpper())
            {
                case "ATTACK": return new AttackSkill();
                case "DEFEND": return new DefendSkill();
                case "WEB SHOOT": return new WebShootSkill();
                default: throw new NotImplementedException();
            }
        }
    }
}
