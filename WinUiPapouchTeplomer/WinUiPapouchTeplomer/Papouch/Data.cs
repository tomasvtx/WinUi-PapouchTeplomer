using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace WinUiPapouchTeplomer.Papouch
{
    /// <summary>
    /// Třída pro správu ukládání a načítání nastavení.
    /// </summary>
    public class SettingsManager
    {
        private ApplicationDataContainer localSettings;

        /// <summary>
        /// Inicializuje novou instanci třídy SettingsManager.
        /// </summary>
        public SettingsManager()
        {
            localSettings = ApplicationData.Current.LocalSettings;
        }

        /// <summary>
        /// Uloží hodnotu nastavení pro zadaný klíč.
        /// </summary>
        /// <param name="key">Klíč, pod kterým bude hodnota uložena.</param>
        /// <param name="value">Hodnota, která má být uložena.</param>
        public void SaveSetting(string key, string value)
        {
            try
            {
                localSettings.Values[key] = value;
            }
            catch { }
        }

        /// <summary>
        /// Načte hodnotu nastavení pro zadaný klíč.
        /// </summary>
        /// <param name="key">Klíč, pro který se má načíst hodnota nastavení.</param>
        /// <returns>Hodnota nastavení nebo null, pokud nastavení nebylo nalezeno.</returns>
        public string LoadSetting(string key)
        {
            try
            {
                if (localSettings.Values.ContainsKey(key))
                {
                    return localSettings.Values[key].ToString();
                }
                return null; // Nastavení nebylo nalezeno
            }
            catch { return null; }
        }
    }
}
