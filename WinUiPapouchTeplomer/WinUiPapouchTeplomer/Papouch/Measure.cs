using Microsoft.UI.Dispatching;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinUiPapouchTeplomer.ViewModel;

namespace WinUiPapouchTeplomer.Papouch
{
    public class Measure
    {
        /// <summary>
        /// Instance sériového portu
        /// </summary>
        public static SerialPort SerialPort { get; set; }

        /// <summary>
        /// Instance senzoru PapouchSensorTM
        /// </summary>
        public static PapouchSensor.PapouchSensorTM sensorTM { get; set; }

        private readonly DispatcherQueue dispatcherQueue;

        public Measure(DispatcherQueue dispatcherQueue)
        {
            this.dispatcherQueue = dispatcherQueue;
        }

        /// <summary>
        /// Metoda pro průběžné měření teploty.
        /// </summary>
        public async Task MeasureTemp()
        {
            try
            {
            Repeat:
                var temperatureData = await ReadTemperatureAsync();
                UpdateViewModel(temperatureData);
                AddTemperatureRecordIfChanged();

                await Task.Delay(50);
                goto Repeat;
            }
            catch { }
        }

        /// <summary>
        /// Metoda pro ukončení měření a čištění zdrojů.
        /// </summary>
        public void Exit()
        {
            CloseSerialPort();
            DisposeSerialPort();
        }

        /// <summary>
        /// Asynchronní metoda pro čtení teplotních dat ze senzoru PapouchSensorTM.
        /// </summary>
        /// <returns>Teplotní data nebo null, pokud se nepodařilo načíst.</returns>
        private async Task<PapouchSensor.PapouchSensorTM.TempOutput?> ReadTemperatureAsync() => await Task.Run(() => sensorTM?.ReadTemperature(ref App.SerialPort));


        /// <summary>
        /// Aktualizuje data v pohledovém modelu (ViewModel) na základě teplotních dat.
        /// </summary>
        /// <param name="temperatureData">Teplotní data ze senzoru PapouchSensorTM.</param>
        private void UpdateViewModel(PapouchSensor.PapouchSensorTM.TempOutput? temperatureData)
        {
            dispatcherQueue.TryEnqueue(DispatcherQueuePriority.Low, () =>
            {
                // Aktualizuje teplotu a chybovou zprávu v pohledovém modelu
                App.ViewModel.Teplota = new ZáznamTeploty
                {
                    Teplota = temperatureData?.Error.Length > 0 ? float.NaN : (temperatureData?.Temperature ?? float.NaN),
                    TeplotaF = temperatureData?.Error.Length > 0 ? float.NaN : ((temperatureData?.Temperature * 9 / 5) + 32 ?? float.NaN)
                };
                App.ViewModel.Chyba = temperatureData?.Error ?? "Připojte a nastavte Papouch teploměr";
            });
        }

        /// <summary>
        /// Přidá nový záznam teploty do kolekce, pokud se teplota změnila a je platná.
        /// </summary>
        private void AddTemperatureRecordIfChanged()
        {
            if (App.ViewModel?.Teploty?.Count == 0 || App.ViewModel?.Teploty?.LastOrDefault()?.Teplota != App.ViewModel?.Teplota?.Teplota)
            {
                if (IsTemperatureValid(App.ViewModel.Teplota?.Teplota))
                {
                    dispatcherQueue.TryEnqueue(DispatcherQueuePriority.Low, () =>
                    {
                        // Přidá nový záznam teploty s aktuálním časem do kolekce
                        App.ViewModel.Teploty.Add(new ZáznamTeploty
                        {
                            Teplota = App.ViewModel.Teplota.Teplota,
                            DateTime = DateTime.Now,
                            TeplotaF = App.ViewModel.Teplota.TeplotaF
                        });
                    });
                }
            }
        }

        /// <summary>
        /// Zkontroluje, zda je teplota platná.
        /// </summary>
        /// <param name="temperature">Teplota k ověření.</param>
        /// <returns>True, pokud je teplota platná; jinak false.</returns>
        private bool IsTemperatureValid(float? temperature) => temperature.HasValue && temperature.Value > -50 && temperature.Value < 150;


        /// <summary>
        /// Uzavře sériový port pro komunikaci se senzorem PapouchSensorTM.
        /// </summary>
        private void CloseSerialPort() => Papouch.Measure.SerialPort?.Close();

        /// <summary>
        /// Uvolní prostředky spojené se sériovým portem pro komunikaci se senzorem PapouchSensorTM.
        /// </summary>
        private void DisposeSerialPort() => Papouch.Measure.SerialPort?.Dispose();

    }
}
