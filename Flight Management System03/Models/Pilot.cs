using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Management_System03.Models
{
    public class Pilot
    {
        public int pilotId { get; set; } //generated system
        public string pilotName { get; set; } //user input
        public string pilotPhone { get; set; }//user input
        public string licenseNumber { get; set; }//user input
        public int flightHours { get; set; } = 0; //default value
        public bool isAvailable { get; set; } = true; //default value 
    }
}
