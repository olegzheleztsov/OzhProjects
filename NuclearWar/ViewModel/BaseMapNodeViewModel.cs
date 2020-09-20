using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuclearWar.ViewModel
{
    public class BaseMapNodeViewModel : ViewModelBase
    {
        private double x;
        private double y;
        private double width;
        private double height;

        public double X
        {
            get => x;
            set => Set<double>(nameof(X), ref x, value);
        }

        public double Y
        {
            get => y;
            set => Set<double>(nameof(Y), ref y, value);
        }

        public double Width
        {
            get => width;
            set => Set(nameof(Width), ref width, value);
        }

        public double Height
        {
            get => height;
            set => Set(nameof(Height), ref height, value);
        }
    }
}
