using System.Collections.Generic;
using System.Linq;

namespace Framework.BattleSystem.Dungeon
{
    public class DungeonLevel
    {
        // Properties
        public List<BattleCharacter> Enemies { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public DungeonLevel(List<BattleCharacter> enemies)
        {
            Enemies = enemies;
        }

        /// <summary>
        /// Determine if the dungeon level is cleared
        /// </summary>
        public bool IsCleared()
        {
            return Enemies.All(x => !x.IsAlive());
        }
    }
}
