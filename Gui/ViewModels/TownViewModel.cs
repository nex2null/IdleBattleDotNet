using Framework;
using System.Windows.Threading;

namespace Gui.ViewModels
{
    public class TownViewModel : BaseViewModel
    {
        // Properties
        public int TotalExperience { get; private set; }
        public int TotalGold { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public TownViewModel(Dispatcher windowDispatcher) : base(windowDispatcher)
        {
            // Subscribe to town update event and do initial update
            Game.Instance.Town.TownUpdate += OnTownUpdate;
            OnTownUpdate();
        }

        /// <summary>
        /// Handles the town updating
        /// </summary>
        public void OnTownUpdate()
        {
            WindowDispatcher.Invoke(() =>
            {
                TotalExperience = Game.Instance.Town.TotalExperience;
                TotalGold = Game.Instance.Town.TotalGold;
            });
        }
    }
}
