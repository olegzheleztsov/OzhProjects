using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorLib.Dtos
{
    public class ElevatorStateDto
    {
        public int PersonCount { get; set; }
        public int CurrentFloor { get; set; }
        public int TargetFloor { get; set; }
        public string State { get; set; }
    }
}
