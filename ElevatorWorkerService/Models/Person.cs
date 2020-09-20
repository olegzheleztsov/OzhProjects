using System;
using System.Text;

namespace ElevatorWorkerService.Models
{
    public class Person : IPerson
    {
        public PersonState State { get; private set; } = PersonState.Unknown;

        public string Id { get; }

        public int SourceFloor { get; }

        public int TargetFloor { get; }

        public int CurrentFloor { get; private set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Person {Id} ==>");
            stringBuilder.AppendLine($"Source Floor: {SourceFloor}, Target Floor: {TargetFloor}|   Current Floor: {CurrentFloor}");
            return stringBuilder.ToString();
        }

        public Person(int sourceFloor, int targetFloor)
        {
            Id = Guid.NewGuid().ToString();
            SourceFloor = sourceFloor;
            TargetFloor = targetFloor;
            CurrentFloor = sourceFloor;
        }

        public void SetState(PersonState state)
        {
            State = state;
        }

        public void SetFloor(int floor )
        {
            CurrentFloor = floor;
        }
    }
}
