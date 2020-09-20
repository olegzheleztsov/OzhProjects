// Create By: Oleg Gelezcov                        (olegg )
// Project: ElevatorLib     File: SettingsDto.cs    Created at 2020/08/26/2:23 AM
// All rights reserved, for personal using only
// 

namespace ElevatorLib.Dtos
{
    public class SettingsDto
    {
        public float GenerationFillingRate { get; set; }
        public float GenerationCheckInterval { get; set; }
        public int MaxPersonsPerBuilding { get; set; }
        public float IntervalBetweenGenerationSinglePerson { get; set; }
    }
}