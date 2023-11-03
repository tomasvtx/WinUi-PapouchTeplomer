using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.IO.Ports;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUiPapouchTeplomer
{
    public sealed partial class MainWindow : Window
    {
        /// <summary>
        /// Inicializuje novou instanci t��dy <see cref="MainWindow"/>.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            InitializeWindow();
        }

        /// <summary>
        /// Inicializuje hlavn� okno aplikace a nastavuje jeho datacontext na viewmodel.
        /// </summary>
        private void InitializeWindow()
        {
            Grid.DataContext = App.ViewModel;
            App.ViewModel.Ports = SerialPort.GetPortNames();
            Closed += MainWindowClosed;
        }

        /// <summary>
        /// Obsluhuje ud�lost uzav�en� hlavn�ho okna aplikace a prov�d� pokus o ukon�en� m��en�.
        /// </summary>
        private void MainWindowClosed(object sender, WindowEventArgs args) => TryExitMeasurement();

        /// <summary>
        /// Obsluhuje ud�lost zm�ny v�b�ru v ComboBoxu a prov�d� pokus o ukon�en� m��en� a inicializaci teplotn�ho senzoru.
        /// </summary>
        private async void ComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TryExitMeasurement();
            await InitializeTemperatureSensorAsync();
        }

        /// <summary>
        /// Prov�d� pokus o ukon�en� m��en� teploty.
        /// </summary>
        private void TryExitMeasurement()
        {
            try
            {
                App.TemperatureMeasure.Exit();
            }
            catch { }
        }

        /// <summary>
        /// Inicializuje teplotn� senzor asynchronn� a ukl�d� vybran� port do nastaven� aplikace.
        /// </summary>
        private async Task InitializeTemperatureSensorAsync()
        {
            try
            {
                await Task.Delay(100); // Zpo�d�n� pro bezpe�n� uzav�en� a uvoln�n� s�riov�ho portu
                App.TemperatureMeasure.Exit();
                Papouch.Measure.sensorTM = new PapouchSensor.PapouchSensorTM(App.ViewModel.Port);
                App.SettingsManager.SaveSetting("PORT", App.ViewModel.Port);
            }
            catch { }
        }

        /// <summary>
        /// Obsluhuje ud�lost zm�ny v�b�ru v druh�m ComboBoxu a ukl�d� vybranou metodu do nastaven� aplikace.
        /// </summary>
        private async void ComboBoxSelectionChanged1(object sender, SelectionChangedEventArgs e)
        {
            await Task.Delay(100);
            App.SettingsManager.SaveSetting("MEASURE", App.ViewModel.SelectedMethod);
        }
    }
}
