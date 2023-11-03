using System;

namespace WinUiPapouchTeplomer.ViewModel
{
    /// <summary>
    /// Reprezentuje záznam teploty s časovým razítkem a teplotní hodnotou.
    /// </summary>
    public class ZáznamTeploty
    {
        /// <summary>
        /// Časové razítko záznamu teploty.
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Teplotní hodnota záznamu teploty v stupních Celsius.
        /// </summary>
        public float Teplota { get; set; }

        /// <summary>
        /// Teplotní hodnota záznamu teploty v stupních Fahrenheita.
        /// </summary>
        public float TeplotaF { get; set; }
    }
}
