using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Client_ML_Gesture_Sensors.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
