using Framework.BattleSystem.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.BattleSystem
{
    public class Battle
    {
        // Properties
        public List<BattleCharacter> PlayerCharacters { get; set; }
        public List<BattleCharacter> AllCharactersInCurrentLevel { get; set; }
        public Dungeon.Dungeon Dungeon { get; set; }
        public BattleLog BattleLog { get; set; }
        public BattleStateEnum CurrentState { get; set; }

        // Events
        public event Action<BattleStateEnum> OnStateChange;

        /// <summary>
        /// Constructor
        /// </summary>
        public Battle(
            List<BattleCharacter> playerCharacters,
            Dungeon.Dungeon dungeon,
            BattleLog battleLog)
        {
            PlayerCharacters = playerCharacters;
            Dungeon = dungeon;
            BattleLog = battleLog;

            ChangeState(BattleStateEnum.BattleBegin);
            UpdateCharactersInCurrentLevel();
        }

        /// <summary>
        /// Changes the battle state
        /// </summary>
        private void ChangeState(BattleStateEnum state)
        {
            // Verify the current state actually changes
            if (CurrentState == state)
                return;

            // Update the state and fire event
            CurrentState = state;
            OnStateChange?.Invoke(state);
        }

        /// <summary>
        /// Starts the battle
        /// </summary>
        public async Task Start()
        {
            if (CurrentState == BattleStateEnum.BattleBegin)
            {
                ChangeState(BattleStateEnum.InBattle);
                await ProcessBattle();
            }
        }

        /// <summary>
        /// Processes the battle
        /// </summary>
        private async Task ProcessBattle()
        {
            await Task.Run(() =>
            {
                while (CurrentState == BattleStateEnum.InBattle)
                {
                    // Sleep for a bit
                    Thread.Sleep(10);

                    // Update all charges
                    UpdateAllCharges();

                    // Grab the next character to act and verify we can find one
                    var readyCharacter = AllCharactersInCurrentLevel.FirstOrDefault(x => x.IsReadyToAct());
                    if (readyCharacter == null)
                        continue;

                    // Have the character act
                    readyCharacter.Act(AllCharactersInCurrentLevel, BattleLog);

                    // Determine the next battle state post-action
                    ChangeState(DetermineStateAfterAction());
                }
            });            
        }

        /// <summary>
        /// Updates all character charges
        /// </summary>
        private void UpdateAllCharges()
        {
            AllCharactersInCurrentLevel.ForEach(x => x.UpdateCharge());
        }

        /// <summary>
        /// Gets all the characters in the current dungeon level
        /// </summary>
        private void UpdateCharactersInCurrentLevel()
        {
            AllCharactersInCurrentLevel = PlayerCharacters.Union(Dungeon.CurrentLevel.Enemies).ToList();
        }

        /// <summary>
        /// Determines the battle state after an action has been performed
        /// </summary>
        private BattleStateEnum DetermineStateAfterAction()
        {
            // If all the player characters are dead, then the battle is lost
            if (PlayerCharacters.FirstOrDefault(x => x.IsAlive()) == null)
            {
                BattleLog.AddMessage("The party is defeated...");
                return BattleStateEnum.BattleLost;
            }

            // If we have cleared the dungeon then we have won
            if (Dungeon.IsCleared())
            {
                BattleLog.AddMessage("The dungeon has been cleared!");
                return BattleStateEnum.BattleWon;
            }

            // If we can advance to the next level then check for advancement
            if (Dungeon.CanAdvanceToNextLevel())
            {
                BattleLog.AddMessage("The dungeon level has been cleared");
                return BattleStateEnum.LevelCleared;
            }

            // We are still fighting on the current level
            return BattleStateEnum.InBattle;
        }

        // Determine if the battle is won
        public bool IsBattleWon()
        {
            return CurrentState == BattleStateEnum.BattleWon;
        }

        // Determine if the battle is lost
        public bool IsBattleLost()
        {
            return CurrentState == BattleStateEnum.BattleLost;
        }

        // Determine if we are waiting for the user to advance
        public bool IsWaitingOnUserAdvancement()
        {
            return CurrentState == BattleStateEnum.LevelCleared;
        }

        // Advance to the next level
        public async void AdvanceLevel()
        {
            // Verify we can advance
            if (CurrentState != BattleStateEnum.LevelCleared)
                return;

            // Advance to the next level
            Dungeon.AdvanceToNextLevel();
            UpdateCharactersInCurrentLevel();

            // Process the batle
            ChangeState(BattleStateEnum.InBattle);
            await ProcessBattle();
        }
    }
}
