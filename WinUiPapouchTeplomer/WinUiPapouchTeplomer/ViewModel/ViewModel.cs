using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace WinUiPapouchTeplomer.ViewModel
{
    /// <summary>
    /// Třída ViewModel slouží k uchovávání dat a propojení s uživatelským rozhraním.
    /// </summary>
    public class ViewModel : ViewModelBase
    {
        private ZáznamTeploty teplota;
        private ObservableCollection<ZáznamTeploty> teploty;
        private string[] ports;
        private string chyba;
        private string port;
        private List<string> method;
        private string selectedmethod;

        /// <summary>
        /// Inicializuje novou instanci třídy ViewModel.
        /// </summary>
        public ViewModel()
        {
            Teploty = new ObservableCollection<ZáznamTeploty>();
            Method = new List<string>();
            Method.Add("Fahrenheit");
            Method.Add("Celsius");

            SelectedMethod = "Celsius";
        }

        /// <summary>
        /// Získá nebo nastaví teplotu.
        /// </summary>
        public ZáznamTeploty Teplota
        {
            get => teplota;
            set => SetProperty(ref teplota, value);
        }

        /// <summary>
        /// Získá nebo nastaví kolekci teplotních záznamů.
        /// </summary>
        public ObservableCollection<ZáznamTeploty> Teploty
        {
            get => teploty;
            set => SetProperty(ref teploty, value);
        }

        /// <summary>
        /// Získá nebo nastaví dostupné porty.
        /// </summary>
        public string[] Ports
        {
            get => ports;
            set => SetProperty(ref ports, value);
        }

        /// <summary>
        /// Získá nebo nastaví vybraný port.
        /// </summary>
        public string Port
        {
            get => port;
            set => SetProperty(ref port, value);
        }

        /// <summary>
        /// Získá nebo nastaví chybovou zprávu.
        /// </summary>
        public string Chyba
        {
            get => chyba;
            set => SetProperty(ref chyba, value);
        }
        public List<string> Method
        {
            get => method;
            set => SetProperty(ref method, value);
        }

        public string SelectedMethod
        {
            get => selectedmethod;
            set => SetProperty(ref selectedmethod, value);
        }

        /// <summary>
        /// Metoda pro nastavení vlastnosti s notifikací o změně.
        /// </summary>
        /// <typeparam name="T">Typ vlastnosti.</typeparam>
        /// <param name="field">Odkaz na vlastnost.</param>
        /// <param name="value">Hodnota, na kterou se má vlastnost nastavit.</param>
        /// <param name="propertyName">Název vlastnosti (nepovinný).</param>
        private void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);
            }
        }
    }
}
