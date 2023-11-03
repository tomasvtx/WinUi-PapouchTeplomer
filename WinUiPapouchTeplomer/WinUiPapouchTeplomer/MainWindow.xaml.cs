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
        /// Inicializuje novou instanci tøídy <see cref="MainWindow"/>.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            InitializeWindow();
        }

        /// <summary>
        /// Inicializuje hlavní okno aplikace a nastavuje jeho datacontext na viewmodel.
        /// </summary>
        private void InitializeWindow()
        {
            Grid.DataContext = App.ViewModel;
            App.ViewModel.Ports = SerialPort.GetPortNames();
            Closed += MainWindowClosed;
        }

        /// <summary>
        /// Obsluhuje událost uzavøení hlavního okna aplikace a provádí pokus o ukonèení mìøení.
        /// </summary>
        private void MainWindowClosed(object sender, WindowEventArgs args) => TryExitMeasurement();

        /// <summary>
        /// Obsluhuje událost zmìny výbìru v ComboBoxu a provádí pokus o ukonèení mìøení a inicializaci teplotního senzoru.
        /// </summary>
        private async void ComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TryExitMeasurement();
            await InitializeTemperatureSensorAsync();
        }

        /// <summary>
        /// Provádí pokus o ukonèení mìøení teploty.
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
        /// Inicializuje teplotní senzor asynchronnì a ukládá vybraný port do nastavení aplikace.
        /// </summary>
        private async Task InitializeTemperatureSensorAsync()
        {
            try
            {
                await Task.Delay(100); // Zpoždìní pro bezpeèné uzavøení a uvolnìní sériového portu
                App.TemperatureMeasure.Exit();
                Papouch.Measure.sensorTM = new PapouchSensor.PapouchSensorTM(App.ViewModel.Port);
                App.SettingsManager.SaveSetting("PORT", App.ViewModel.Port);
            }
            catch { }
        }

        /// <summary>
        /// Obsluhuje událost zmìny výbìru v druhém ComboBoxu a ukládá vybranou metodu do nastavení aplikace.
        /// </summary>
        private async void ComboBoxSelectionChanged1(object sender, SelectionChangedEventArgs e)
        {
            await Task.Delay(100);
            App.SettingsManager.SaveSetting("MEASURE", App.ViewModel.SelectedMethod);
        }
    }
}
