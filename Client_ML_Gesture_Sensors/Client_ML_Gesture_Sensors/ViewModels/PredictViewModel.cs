//using System.Threading.Tasks;
//using System.Windows.Input;
//using Xamarin.Forms;

//using Client_ML_Gesture_Sensors.Models;
//using Client_ML_Gesture_Sensors.Services;

//namespace Sensors_Client.ViewModels
//{
//    public class PredictViewModel
//    {
//        private PredictionService _predictionService;
//        public ICommand PredictCommand { get; }

//        private string _Type;

//        public string Type
//        {
//            get { return _Type; }
//            set
//            {
//                _Type = value;
//                OnPropertyChanged();
//            }
//        }

//        private string _FixedAcidity;

//        public string FixedAcidity
//        {
//            get { return _FixedAcidity; }
//            set
//            {
//                _FixedAcidity = value;
//                OnPropertyChanged();
//            }
//        }

//        private string _VolatileAcidity;

//        public string VolatileAcidity
//        {
//            get { return _VolatileAcidity; }
//            set
//            {
//                _VolatileAcidity = value;
//                OnPropertyChanged();
//            }
//        }

//        private string _CitricAcid;

//        public string CitricAcid
//        {
//            get { return _CitricAcid; }
//            set
//            {
//                _CitricAcid = value;
//                OnPropertyChanged();
//            }
//        }

//        private string _ResidualSugar;

//        public string ResidualSugar
//        {
//            get { return _ResidualSugar; }
//            set
//            {
//                _ResidualSugar = value;
//                OnPropertyChanged();
//            }
//        }

//        private string _Chlorides;

//        public string Chlorides
//        {
//            get { return _Chlorides; }
//            set
//            {
//                _Chlorides = value;
//                OnPropertyChanged();
//            }
//        }

//        private string _FreeSulferDioxide;

//        public string FreeSulferDioxide
//        {
//            get { return _FreeSulferDioxide; }
//            set
//            {
//                _FreeSulferDioxide = value;
//                OnPropertyChanged();
//            }
//        }

//        private string _TotalSulferDioxide;

//        public string TotalSulferDioxide
//        {
//            get { return _TotalSulferDioxide; }
//            set
//            {
//                _TotalSulferDioxide = value;
//                OnPropertyChanged();
//            }
//        }

//        private string _Density;

//        public string Density
//        {
//            get { return _Density; }
//            set
//            {
//                _Density = value;
//                OnPropertyChanged();
//            }
//        }

//        private string _Ph;

//        public string Ph
//        {
//            get { return _Ph; }
//            set
//            {
//                _Ph = value;
//                OnPropertyChanged();
//            }
//        }

//        private string _Sulphates;

//        public string Sulphates
//        {
//            get { return _Sulphates; }
//            set
//            {
//                _Sulphates = value;
//                OnPropertyChanged();
//            }
//        }

//        private string _Alcohol;

//        public string Alcohol
//        {
//            get { return _Alcohol; }
//            set
//            {
//                _Alcohol = value;
//                OnPropertyChanged();
//            }
//        }

//        public PredictViewModel()
//        {
//            PredictCommand = new Command(async () => await ExecutePredictCommand());

//            _predictionService = new PredictionService();
//        }

//        private async Task ExecutePredictCommand()
//        {
//            var data = new Gesture
//            {
//                Type = Type,
//                FixedAcidity = FixedAcidity,
//                VolatileAcidity = VolatileAcidity,
//                CitricAcid = CitricAcid,
//                ResidualSugar = ResidualSugar,
//                Chlorides = Chlorides,
//                FreeSulfurDioxide = FreeSulferDioxide,
//                TotalSulforDioxide = TotalSulferDioxide,
//                Density = Density,
//                Ph = Ph,
//                Sulphates = Sulphates,
//                Alcohol = Alcohol
//            };

//            var quality = await _predictionService.Predict(data);

//            //MessagingCenter.Send(this, "Predict", quality);
//        }
//    }
//}
