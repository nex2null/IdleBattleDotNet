using System.Collections.Generic;
using System.Linq;

namespace Framework.BattleSystem.Dungeon
{
    public class Dungeon
    {
        // Properties
        public int DifficultyLevel { get; set; }
        public List<DungeonLevel> Levels { get; set; }
        public DungeonLevel CurrentLevel { get; set; }
        public int CurrentLevelNumber { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Dungeon(int difficultyLevel, List<DungeonLevel> dungeonLevels)
        {
            DifficultyLevel = difficultyLevel;
            Levels = dungeonLevels;
            CurrentLevel = dungeonLevels.First();
            CurrentLevelNumber = 1;
        }

        /// <summary>
        /// Determine if the dungeon can be advanced to the next level
        /// </summary>
        public bool CanAdvanceToNextLevel()
        {
            return CurrentLevel.IsCleared() && Levels.Count > CurrentLevelNumber;
        }

        /// <summary>
        /// Advance to the next level
        /// </summary>
        public void AdvanceToNextLevel()
        {
            if (!CanAdvanceToNextLevel())
                return;

            CurrentLevelNumber++;
            CurrentLevel = Levels[CurrentLevelNumber - 1];
        }

        /// <summary>
        /// Determine if the dungeon has been cleared
        /// </summary>
        public bool IsCleared()
        {
            return Levels.Last().IsCleared();
        }

        /// <summary>
        /// Gets the defeated enemies in the dungeon
        /// </summary>
        public List<BattleCharacter> GetDefeatedEnemies()
        {
            return Levels
                .SelectMany(x => x.Enemies)
                .Where(x => !x.IsAlive())
                .ToList();
        }
    }
}
