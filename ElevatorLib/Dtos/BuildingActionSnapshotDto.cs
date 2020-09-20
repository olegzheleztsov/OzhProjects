using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorLib.Dtos
{
    public class BuildingActionSnaphotDto
    {
        public string Action { get; set; }
        public BuildingStateDto Building { get; set; }
    }
}
