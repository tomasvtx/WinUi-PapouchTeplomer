using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using System.IO.Ports;
using System.Threading.Tasks;
using WinUiPapouchTeplomer.Converter;
using WinUiPapouchTeplomer.Papouch;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUiPapouchTeplomer
{
    /// <summary>
    /// Inicializuje novou instanci třídy <see cref="App"/>.
    /// </summary>
    public partial class App : Application
    {
        public static ViewModel.ViewModel ViewModel;
        public static Papouch.Measure TemperatureMeasure;
        public static Papouch.SettingsManager SettingsManager;
        private Window mainWindow;
        internal static SerialPort SerialPort;

        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Obsluhuje spuštění aplikace a provádí inicializaci komponent.
        /// </summary>
        /// <param name="args">Informace o spuštění aplikace.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            try
            {
                InitializeAppComponents();
                InitializeResources();
                CreateAndActivateMainWindow();
                InitializeViewModelPort();
                StartTemperatureMeasurement();
            }
            catch { }
        }

        /// <summary>
        /// Inicializuje komponenty aplikace, včetně viewmodelu, správce nastavení a měření teploty.
        /// </summary>
        private void InitializeAppComponents()
        {
            try
            {
                ViewModel = new ViewModel.ViewModel();
                SettingsManager = new Papouch.SettingsManager();
                TemperatureMeasure = new Papouch.Measure(DispatcherQueue.GetForCurrentThread());
            }
            catch { }
        }


        /// <summary>
        /// Inicializuje prostředky aplikace, včetně konvertérů pro zobrazení teploty.
        /// </summary>
        private void InitializeResources()
        {
            try
            {
                var resources = new Microsoft.UI.Xaml.ResourceDictionary();
                resources.Add("DoubleToTextConverter", new DoubleToTextConverter());
                resources.Add("CVisible", new CVisible());
                resources.Add("FVisible", new FVisible());
                Resources.MergedDictionaries.Add(resources);
            }
            catch { }
        }


        /// <summary>
        /// Vytvoří a aktivuje hlavní okno aplikace.
        /// </summary>
        private void CreateAndActivateMainWindow()
        {
            try
            {
                mainWindow = new MainWindow();
                mainWindow.Activate();
            }
            catch { }
        }


        /// <summary>
        /// Inicializuje port v viewmodelu na základě uloženého nastavení.
        /// </summary>
        private void InitializeViewModelPort()
        {
            ViewModel.Port = SettingsManager.LoadSetting("PORT");
            ViewModel.SelectedMethod = SettingsManager.LoadSetting("MEASURE");
        }


        /// <summary>
        /// Spustí měření teploty asynchronně.
        /// </summary>
        private async void StartTemperatureMeasurement() => await TemperatureMeasure.MeasureTemp();
    }
}
