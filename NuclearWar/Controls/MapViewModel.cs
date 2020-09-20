using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NuclearWar.Infrastructure;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using NuclearWar.ViewModel;
using System.Diagnostics;

namespace NuclearWar.Controls
{
    public class MapViewModel : ViewModelBase
    {
        private double zoomRate = 1.02;
        private double zoomX = 1.0;
        private double zoomY = 1.0;
        private double translateX = 0.0;
        private double translateY = 0.0;
        private bool isPanning = false;
        private double previousX = 0.0;
        private double previousY = 0.0;
        private UIHelper helper;
        private ObservableCollection<BaseMapNodeViewModel> mapObjects;

        public MapViewModel(UIHelper uIHelper)
        {
            helper = uIHelper;
            
            mapObjects = new ObservableCollection<BaseMapNodeViewModel>(new List<BaseMapNodeViewModel>
            {
                new WorldNodeViewModel
                {
                     X = 0,
                     Y = 0,
                     Width=3840,
                     Height=1799
                },
                new NuclearCityViewModel
                {
                    X = 100,
                    Y = 100,
                    Width = 70,
                    Height = 70
                }
            });

            ZoomMap = new RelayCommand<MouseWheelEventArgs>(args => {
                var delta = args.Delta;
                if(delta > 0)
                {
                    ZoomX *= ZoomRate;
                    ZoomY *= ZoomRate;
                } else
                {
                    ZoomX /= ZoomRate;
                    ZoomY /= ZoomRate;
                }
            });
            StartPan = new RelayCommand<MouseButtonEventArgs>(OnStartPan);
            Pan = new RelayCommand<MouseEventArgs>(OnPan);
            EndPan = new RelayCommand<MouseButtonEventArgs>(OnEndPan);
        }

        private void OnStartPan(MouseButtonEventArgs args)
        {
            //MessageBox.Show("START");
            if (!isPanning)
            {
                var result = helper.CaptureMouseInParent<Canvas>(args.Source as DependencyObject);
                //MessageBox.Show($"Captures success: {result.success}");
                var border = helper.FindObjectInParent<Border>(args.Source as DependencyObject);
                Debug.WriteLine($"Border founded: {border != null}");
                var mousePosition = args.GetPosition(border);
                previousX = mousePosition.X;
                previousY = mousePosition.Y;
                isPanning = true;
            }
        }

        private void OnPan(MouseEventArgs args)
        {
            if (!isPanning) { return; }
            if (args.LeftButton == MouseButtonState.Pressed && (helper.FindObjectInParent<Canvas>(args.Source)?.IsMouseCaptured ?? false))
            {
                var border = helper.FindObjectInParent<Border>(args.Source as DependencyObject);
                var mousePosition = args.GetPosition(border);
                TranslateX += (mousePosition.X - previousX);
                TranslateY += (mousePosition.Y - previousY);
                previousX = mousePosition.X;
                previousY = mousePosition.Y;
            }
        }

        private void OnEndPan(MouseButtonEventArgs args)
        {
            helper.ReleaseMouseInParent<Canvas>(args.Source as DependencyObject);
            isPanning = false;
        }



        public double ZoomRate
        {
            get => zoomRate;
            set => Set<double>(nameof(ZoomRate), ref zoomRate, value);
        }

        public double ZoomX
        {
            get => zoomX;
            set => Set<double>(nameof(ZoomX), ref zoomX, value);
        }

        public double ZoomY
        {
            get => zoomY;
            set => Set<double>(nameof(ZoomY), ref zoomY, value);
        }

        public double TranslateX
        {
            get => translateX;
            set => Set<double>(nameof(TranslateX), ref translateX, value);
        }

        public double TranslateY
        {
            get => translateY;
            set => Set<double>(nameof(TranslateY), ref translateY, value);
        }

        public ObservableCollection<BaseMapNodeViewModel> MapObjects
        {
            get => mapObjects;
            set => Set(nameof(MapObjects), ref mapObjects, value);
        }

        public RelayCommand<MouseWheelEventArgs> ZoomMap { get; private set; }
        public RelayCommand<MouseButtonEventArgs> StartPan { get; private set; }
        public RelayCommand<MouseEventArgs> Pan { get; private set; }
        public RelayCommand<MouseButtonEventArgs> EndPan { get; private set; }
    }
}
