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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NuclearWar.Controls
{
    /// <summary>
    /// Interaction logic for NuclearCity.xaml
    /// </summary>
    public partial class NuclearCity : UserControl
    {
        public NuclearCity()
        {
            InitializeComponent();
        }

        private static readonly DependencyProperty CityBackgroundProperty = DependencyProperty.Register(
            "CityBackground", typeof(Brush), typeof(NuclearCity), new FrameworkPropertyMetadata(Brushes.White));

        public Brush CityBackground
        {
            get => (Brush)GetValue(CityBackgroundProperty);
            set => SetValue(CityBackgroundProperty, value);
        }
    }
}
