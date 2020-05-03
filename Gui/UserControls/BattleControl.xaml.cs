using Gui.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Gui.UserControls
{
    /// <summary>
    /// User control for representing a battle
    /// </summary>
    public partial class BattleControl : UserControl
    {
        // View model
        public BattleViewModel ViewModel { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public BattleControl()
        {
            InitializeComponent();
            ViewModel = new BattleViewModel(Dispatcher);
            DataContext = ViewModel;

            ViewModel.BattleMessages.CollectionChanged += BattleMessages_CollectionChanged;
        }

        /// <summary>
        /// Handle auto-scrolling the battle log
        /// </summary>
        private void BattleMessages_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var border = (Border)VisualTreeHelper.GetChild(listBox, 0);
                var scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
                scrollViewer.ScrollToBottom();
            });
        }

        /// <summary>
        /// Handler for the start battle button click
        /// </summary>
        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.StartBattle();
        }

        /// <summary>
        /// Handler for the advance battle button click
        /// </summary>
        private void advanceButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AdvanceBattle();
        }

        /// <summary>
        /// Handler for the leave battle button click
        /// </summary>
        private void leaveButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.LeaveBattle();
        }
    }
}
