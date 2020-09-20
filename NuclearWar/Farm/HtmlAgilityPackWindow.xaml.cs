using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using NuclearWar.Services;

namespace NuclearWar.Farm
{
    /// <summary>
    /// Interaction logic for HtmlAgilityPackWindow.xaml
    /// </summary>
    public partial class HtmlAgilityPackWindow : Window
    {
        public HtmlAgilityPackWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var latitudeString = "08.50S";
            var longitudeString = "13.15E";
            StringToCoordinateConverter converter = new StringToCoordinateConverter();
            (double lat, double lon) = converter.Convert(latitudeString, longitudeString);
            outputText.Text = $"{lat}, {lon}";
        }

        private async void btnGetCapitals_Click(object sender, RoutedEventArgs e)
        {
            var path = "https://lab.lmnixon.org/4th/worldcapitals.html";
            IWebService service = new WebService(path);
            var result = await service.GetCapitalsAsync().ConfigureAwait(false);
            await Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, (Action)(() =>
            {
                var stringBuilder = new StringBuilder();
                foreach (var capital in result)
                {
                    stringBuilder.AppendLine(capital.ToString());
                }

                outputText.Text = stringBuilder.ToString();
                MessageBox.Show($"cities count: {result.Count()}");
            }));
        }


    }
}
