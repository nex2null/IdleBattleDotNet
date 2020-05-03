using Framework.BattleSystem;
using Framework.BattleSystem.Dungeon;
using Framework.BattleSystem.Enemies;
using Framework.Itemization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework
{
    public class Game
    {
        // Properties
        public Town Town { get; set; }
        public Battle CurrentBattle { get; set; }

        // Events
        public event Action OnBattleStart;
        public event Action OnBattleEnd;

        // Singleton boilerplate
        private static Game _instance;
        public static Game Instance => _instance ?? (_instance = new Game());

        /// <summary>
        /// Constructor
        /// </summary>
        private Game()
        {
            Town = new Town();
            CurrentBattle = null;
        }

        /// <summary>
        /// Initializes a new battle
        /// </summary>
        public void InitializeBattle(int dungeonId)
        {
            // Verify a battle is not already initialized
            if (CurrentBattle != null)
                return;

            // Create the dungeon
            var dungeon = new Dungeon(1, new List<DungeonLevel>
            {
                new DungeonLevel(new List<BattleCharacter> { new Spider("Spider 1"), new Spider("Spider 2") }),
                new DungeonLevel(new List<BattleCharacter> { new Spider("Spider 3") }),
                new DungeonLevel(new List<BattleCharacter> { new Spider("Spider 4"), new Spider("Spider 5") })
            });

            // Create the battle
            var battleLog = new BattleLog();
            var playerBattleCharacters = Town.PlayerCharacters.Select(x => x.ToBattleCharacter());
            CurrentBattle = new Battle(playerBattleCharacters.ToList(), dungeon, battleLog);
        }

        /// <summary>
        /// Starts the battle
        /// </summary>
        public async Task StartBattle()
        {
            OnBattleStart?.Invoke();
            await CurrentBattle.Start();
        }

        /// <summary>
        /// Leaves the battle
        /// </summary>
        public void LeaveBattle()
        {
            // Make sure we're in a battle
            if (CurrentBattle == null)
                return;

            // If we didn't lose the battle then get rewards
            if (!CurrentBattle.IsBattleLost())
            {
                // Modify XP by the number of levels we completed
                var dungeon = CurrentBattle.Dungeon;
                var xpModified = dungeon.CurrentLevelNumber / dungeon.Levels.Count * 100;

                // Get XP and gold
                var dungeonDifficulty = CurrentBattle.Dungeon.DifficultyLevel;
                Town.UpdateExperience(dungeonDifficulty * dungeonDifficulty * xpModified);

                // Get Items
                var defeatedEnemies = dungeon.GetDefeatedEnemies();
                foreach (var enemy in defeatedEnemies)
                {
                    var items = LootGenerator.GenerateLoot(enemy.MaxNumberOfItemsToDrop, enemy.LootGenerationOptions);
                    Town.Inventory.AddItems(items);
                }
            }

            // Leave the battle
            OnBattleEnd?.Invoke();
            CurrentBattle = null;
        }
    }
}
