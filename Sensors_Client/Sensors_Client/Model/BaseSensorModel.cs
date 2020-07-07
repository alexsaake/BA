using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Sensors_Client.Model
{
    public class BaseSensorModel : INotifyPropertyChanged
    {
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
