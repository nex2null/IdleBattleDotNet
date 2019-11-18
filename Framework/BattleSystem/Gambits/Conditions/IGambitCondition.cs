using System.Collections.Generic;

namespace Framework.BattleSystem.Gambits.Conditions
{
    public interface IGambitCondition
    {
        // Properties
        string Name { get; }
        bool RequiresInput { get; }

        /// <summary>
        /// Gets the targets for the gambit
        /// </summary>
        List<BattleCharacter> GetTargets(BattleCharacter user, List<BattleCharacter> characters);
    }
}
