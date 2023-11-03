using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinUiPapouchTeplomer.ViewModel;

namespace WinUiPapouchTeplomer.Converter
{
    public class CVisible : IValueConverter
    {
        /// <summary>
        /// Konvertuje hodnotu na hodnotu typu Visibility na základě vstupního řetězce.
        /// </summary>
        /// <param name="value">Hodnota pro konverzi.</param>
        /// <param name="targetType">Cílový typ, na který má být hodnota konvertována.</param>
        /// <param name="parameter">Parametr pro konverzi (nepoužívá se).</param>
        /// <param name="language">Jazyk pro konverzi (nepoužívá se).</param>
        /// <returns>Visible, pokud je vstupní řetězec "Celsius"; jinak Collapsed.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string floatValue)
            {
                return floatValue == "Celsius" ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Visible;
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
