using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorWorkerService.Models
{
    public class PersonAction
    {
        public IPerson Person { get; }

        public IFloor Floor { get; }
        public PersonActionType Action { get; }

        public PersonAction(IPerson person, IFloor floor, PersonActionType action)
        {
            Person = person;
            Action = action;
            Floor = floor;
        }

        public override string ToString()
        {
            return $"Action: {Action}, Floor: {Floor?.FloorNumber ?? -1}{Environment.NewLine}{Person.ToString()}";
        }
    }
}
