using System.ComponentModel;

namespace Gui.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
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