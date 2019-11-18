using System.Collections.Generic;

namespace Framework.BattleSystem.Gambits.Conditions
{
    public class SelfCondition : IGambitCondition
    {
        // Properties
        public string Name => "Self";
        public bool RequiresInput => false;

        /// <summary>
        /// Gets the targets for the gambit
        /// </summary>
        public List<BattleCharacter> GetTargets(BattleCharacter user, List<BattleCharacter> characters)
        {
            return new List<BattleCharacter> { user };
        }
    }
}