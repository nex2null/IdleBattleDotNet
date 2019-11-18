using System.Collections.Generic;
using System.Linq;

namespace Framework.BattleSystem.Gambits.Conditions
{
    public class AllyAnyCondition : IGambitCondition
    {
        // Properties
        public string Name => "Ally: Any";
        public bool RequiresInput => false;

        /// <summary>
        /// Gets the targets for the gambit
        /// </summary>
        public List<BattleCharacter> GetTargets(BattleCharacter user, List<BattleCharacter> characters)
        {
            return characters
                .Where(x => x != user && x.CharacterType != user.HostileToCharacterType)
                .ToList();
        }
    }
}
