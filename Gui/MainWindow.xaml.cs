using Gui.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Gui
{
    public partial class MainWindow : Window
    {
        public BattleViewModel ViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new BattleViewModel(Dispatcher);
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