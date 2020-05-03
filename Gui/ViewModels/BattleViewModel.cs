using Framework;
using Framework.BattleSystem.Enums;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace Gui.ViewModels
{
    public class BattleViewModel : BaseViewModel
    {
        // Properties
        public ObservableCollection<string> BattleMessages { get; private set; }
        public bool StartButtonVisible { get; private set; }
        public bool AdvanceButtonVisible { get; private set; }
        public bool LeaveButtonVisible { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public BattleViewModel(Dispatcher windowDispatcher) : base(windowDispatcher)
        {
            BattleMessages = new ObservableCollection<string>();
            StartButtonVisible = true;
        }

        /// <summary>
        /// Starts a new battle
        /// </summary>
        public void StartBattle()
        {
            // Initialize the battle and setup event handlers
            Game.Instance.InitializeBattle(1);
            Game.Instance.CurrentBattle.BattleLog.OnMessageAdded += OnBattleMessage;
            Game.Instance.CurrentBattle.OnStateChange += OnBattleStateChange;

            // Start the battle
            _ = Game.Instance.StartBattle();

            // Hide the start battle button
            StartButtonVisible = false;
        }

        public void AdvanceBattle()
        {
            Game.Instance.CurrentBattle?.AdvanceLevel();
        }

        public void LeaveBattle()
        {
            // Leave the battle
            Game.Instance.LeaveBattle();

            // Clear the battle messages
            BattleMessages.Clear();

            // Reset the button visibilities
            StartButtonVisible = true;
            AdvanceButtonVisible = false;
            LeaveButtonVisible = false;
        }

        /// <summary>
        /// Handles a battle message
        /// </summary>
        public void OnBattleMessage(string message)
        {
            WindowDispatcher.Invoke(() => BattleMessages.Add(message));
        }

        /// <summary>
        /// Handles the battle state changing
        /// </summary>
        public void OnBattleStateChange(BattleStateEnum newState)
        {
            WindowDispatcher.Invoke(() =>
            {
                // Hide all the buttons
                StartButtonVisible = false;
                AdvanceButtonVisible = false;
                LeaveButtonVisible = false;

                // Handle showing the advance button
                if (newState == BattleStateEnum.LevelCleared)
                {
                    AdvanceButtonVisible = true;
                }

                // Handle showing the leave button
                if (newState == BattleStateEnum.BattleLost ||
                    newState == BattleStateEnum.BattleWon ||
                    newState == BattleStateEnum.LevelCleared)
                {
                    LeaveButtonVisible = true;
                }
            });
        }
    }
}