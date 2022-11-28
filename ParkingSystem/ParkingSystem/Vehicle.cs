using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSystem
{
    public class Vehicle
    {
        public string No, Type, Colour;
        public DateTime EntryTime;
        public int Hours;

        public Vehicle(string no, string type, string colour, DateTime entryTime, int hours)
        {
            No = no;
            Type = type;
            Colour = colour;
            EntryTime = entryTime;
            Hours = hours;
        }
    }
}
