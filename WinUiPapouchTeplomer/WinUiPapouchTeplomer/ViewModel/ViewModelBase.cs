using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WinUiPapouchTeplomer.ViewModel
{
    /// <summary>
    /// Základní třída pro viewmodely, která implementuje rozhraní INotifyPropertyChanged pro notifikaci o změně vlastností.
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Událost vyvolaná při změně hodnoty vlastnosti.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Metoda pro notifikaci o změně vlastnosti.
        /// </summary>
        /// <param name="propertyName">Název změněné vlastnosti (volitelný).</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
