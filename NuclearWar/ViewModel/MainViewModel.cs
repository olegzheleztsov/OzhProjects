using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NuclearWar.Controls;
using System.Windows;

namespace NuclearWar.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(UIHelper helper)
        {
            ShowMessage = new RelayCommand(() =>
            {
                MessageBox.Show("Hey");
            });

            Map = new MapViewModel(helper);
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
            ///
        }

        private string _someData = "initial data";
        private MapViewModel map;

        public string SomeData
        {
            get => _someData;
            set
            {
                Set<string>(nameof(SomeData), ref _someData, value);
            }
        }

        public MapViewModel Map
        {
            get => map;
            set => Set<MapViewModel>(nameof(Map), ref map, value);
        }

        public RelayCommand ShowMessage { get; private set; }
    }
}