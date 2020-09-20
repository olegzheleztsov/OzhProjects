using NuclearWar.Controls;
using NuclearWar.Infrastructure;
using System;


namespace NuclearWar.ViewModel
{
    public class WorldNodeViewModel : BaseMapNodeViewModel
    {
        const string DEFAULT_MAP_FILE = "Assets/Images/map.jpg";
        private Uri mapFile;

        public WorldNodeViewModel()
        {
            mapFile = Utils.MakePackUri(typeof(MapViewModel), DEFAULT_MAP_FILE);
        }

        public Uri MapFile
        {
            get => mapFile;
            set => Set<Uri>(nameof(MapFile), ref mapFile, value);
        }
    }
}
