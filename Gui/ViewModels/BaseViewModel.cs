using System.ComponentModel;
using System.Windows.Threading;

namespace Gui.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        // Dispatcher
        protected Dispatcher WindowDispatcher;

        /// <summary>
        /// Constructor
        /// </summary>
        public BaseViewModel(Dispatcher windowDispatcher)
        {
            WindowDispatcher = windowDispatcher;
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
}