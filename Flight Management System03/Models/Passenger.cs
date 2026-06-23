using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Management_System03.Models
{
    public class Passenger
    {
        public int passengerId { get; set; }//generated system
        public string passengerName { get; set; } //user input
        public string passengerEmail { get; set; } //user input
        public string passengerPhone { get; set; } //user input
        public string passportNumber { get; set; } //user input
        public string nationality { get; set; } //user input
    }
}
