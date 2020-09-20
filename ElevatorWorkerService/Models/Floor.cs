using System.Collections.Generic;
using System.Text;

namespace ElevatorWorkerService.Models
{
    public class Floor : IFloor
    {
        public Floor(int floorNumber, int maxPersons)
        {
            FloorNumber = floorNumber;
            MaxPersons = maxPersons;
        }

        private IList<IPerson> Persons { get; } = new List<IPerson>();

        public int MaxPersons { get; set; }

        public int FloorNumber { get; }

        public bool TryAttachToFloor(IPerson person)
        {
            if (Persons.Count >= MaxPersons)
            {
                return false;
            }

            Persons.Add(person);
            return true;
        }

        public int PersonCount => Persons.Count;

        public bool IsFull => PersonCount >= MaxPersons;

        public bool IsEmpty => PersonCount == 0;

        public IEnumerable<IPerson> DetachPersons(int count)
        {
            var detachedPersons = new List<IPerson>();
            for (var i = 0; i < count; i++)
            {
                if (Persons.Count <= 0) continue;
                var person = Persons[0];
                Persons.RemoveAt(0);
                detachedPersons.Add(person);
            }

            return detachedPersons;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(
                $"Floor {FloorNumber}, Max Persons: {MaxPersons}, Current Persons: {Persons.Count}");
            foreach (var person in Persons)
            {
                stringBuilder.AppendLine($"\t{person}");
            }

            return stringBuilder.ToString();
        }

        public bool TryDetachFromFloor(IPerson person)
        {
            if (!Persons.Contains(person)) return false;
            Persons.Remove(person);
            return true;

        }
    }
}