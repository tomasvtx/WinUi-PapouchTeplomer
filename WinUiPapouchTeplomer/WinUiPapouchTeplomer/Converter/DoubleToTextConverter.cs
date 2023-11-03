using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinUiPapouchTeplomer.ViewModel;

namespace WinUiPapouchTeplomer.Converter
{
    public class DoubleToTextConverter : IValueConverter
    {
        /// <summary>
        /// Konvertuje hodnotu float na textový řetězec s jednotkou °C.
        /// </summary>
        /// <param name="value">Hodnota float, která má být konvertována.</param>
        /// <param name="targetType">Cílový typ, na který má být hodnota konvertována.</param>
        /// <param name="parameter">Parametr pro konverzi (nepoužívá se).</param>
        /// <param name="language">Jazyk pro konverzi (nepoužívá se).</param>
        /// <returns>Textový řetězec s hodnotou a jednotkou °C nebo zprávou "Připojte teploměr Papouch".</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is ZáznamTeploty floatValue && !float.IsNaN(floatValue.Teplota))
            {
                float roundedValue = (float)Math.Round(floatValue.Teplota, 1); // Zaokrouhleno na 1 desetinné místo

                return App.ViewModel.SelectedMethod == "Celsius" ? $"{roundedValue} °C" : $"{(roundedValue * 9 / 5 + 32):N1} °F"; // Použijeme zaokrouhlenou hodnotu
            }

            return "Připojte teploměr Papouch";

        }

        /// <summary>
        /// Není implementována zpětná konverze (vždy vrací null).
        /// </summary>
        /// <param name="value">Hodnota pro zpětnou konverzi (nepoužívá se).</param>
        /// <param name="targetType">Cílový typ pro zpětnou konverzi (nepoužívá se).</param>
        /// <param name="parameter">Parametr pro zpětnou konverzi (nepoužívá se).</param>
        /// <param name="language">Jazyk pro zpětnou konverzi (nepoužívá se).</param>
        /// <returns>Null vždy, protože zpětná konverze není implementována.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
