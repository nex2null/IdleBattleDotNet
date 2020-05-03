using Gui.ViewModels;
using System.Windows.Controls;

namespace Gui.UserControls
{
    /// <summary>
    /// User control for representing a town
    /// </summary>
    public partial class TownControl : UserControl
    {
        // View model
        public TownViewModel ViewModel { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public TownControl()
        {
            InitializeComponent();
            ViewModel = new TownViewModel(Dispatcher);
            DataContext = ViewModel;
        }
    }
}
