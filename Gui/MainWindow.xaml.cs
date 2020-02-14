using Framework;
using Framework.BattleSystem.Enums;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Gui
{
    public partial class MainWindow : Window
    {
        public class MyViewModel : INotifyPropertyChanged
        {
            // Properties
            public ObservableCollection<string> BattleMessages { get; private set; }
            public bool StartButtonVisible { get; private set; }
            public bool AdvanceButtonVisible { get; private set; }
            public bool LeaveButtonVisible { get; private set; }

            // Dispatcher
            private Dispatcher _windowDispatcher;

            /// <summary>
            /// Constructor
            /// </summary>
            public MyViewModel(Dispatcher windowDispatcher)
            {
                _windowDispatcher = windowDispatcher;
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
                UpdateProperty("StartButtonVisible");
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
                UpdateProperty("StartButtonVisible");
                UpdateProperty("AdvanceButtonVisible");
                UpdateProperty("LeaveButtonVisible");
            }

            /// <summary>
            /// Handles a battle message
            /// </summary>
            public void OnBattleMessage(string message)
            {
                _windowDispatcher.Invoke(() => BattleMessages.Add(message));
            }

            /// <summary>
            /// Handles the battle state changing
            /// </summary>
            public void OnBattleStateChange(BattleStateEnum newState)
            {
                _windowDispatcher.Invoke(() =>
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

                    // Fire all events
                    UpdateProperty("StartButtonVisible");
                    UpdateProperty("AdvanceButtonVisible");
                    UpdateProperty("LeaveButtonVisible");
                });
            }

            /// <summary>
            /// INotifyPropertyChanged boilerplate
            /// </summary>
            public event PropertyChangedEventHandler PropertyChanged;
            protected void UpdateProperty(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        public MyViewModel ViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MyViewModel(Dispatcher);
            DataContext = ViewModel;

            ViewModel.BattleMessages.CollectionChanged += BattleMessages_CollectionChanged;
        }

        private void BattleMessages_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var border = (Border)VisualTreeHelper.GetChild(listBox, 0);
                var scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
                scrollViewer.ScrollToBottom();
            });
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.StartBattle();
        }

        private void advanceButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AdvanceBattle();
        }

        private void leaveButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.LeaveBattle();
        }
    }
}