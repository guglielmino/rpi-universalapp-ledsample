using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Gpio;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RpiLedSample
{
   

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        bool state = false;
        const int LED_PIN = 18;
        GpioPin pin;


        private void InitGPIO()
        {
            try {
                var gpio = GpioController.GetDefault();

                pin = gpio.OpenPin(LED_PIN);

                pin.SetDriveMode(GpioPinDriveMode.Output);
                pin.Write(GpioPinValue.Low);
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("Inizializzazione GPIO non riuscita");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            state = !state;

            var pinValue = state ? GpioPinValue.High : GpioPinValue.Low;
            if (pin != null)
            {
                pin.Write(pinValue);
            }

            Button btn = (Button)sender;
            btn.Content = state ? "ON" : "OFF";
        }
        public MainPage()
        {
            InitGPIO();
            this.InitializeComponent();
        }
    }
}
