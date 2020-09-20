using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorLib.Dtos
{
    public class BuildingStateDto
    {
        public FloorStateDto[] Floors { get; set; }
        public ElevatorStateDto Elevator { get; set; }
        public int PersonExitedCount { get; set; }
        public int PersonNonExitedCount { get; set; }

    }
}
