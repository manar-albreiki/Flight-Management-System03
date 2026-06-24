using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Management_System03.Models
{
    public class Flight
    {
        public int flightId { get; set; } //generated system
        public string flightCode { get; set; } //generated system
        public int aircraftId { get; set; } //user input from the list
        public int pilotId { get; set; }//user input from the list
        public string origin { get; set; } //user input
        public string destination { get; set; } //user input
        public string departureDate { get; set; } //user input
        public string departureTime { get; set; } //user input
        public decimal ticketPrice { get; set; } //user input
        public int availableSeats { get; set; }//calculated from totalSeats(aircraftId)

        public string status { get; set; } //default value, Flight status: Scheduled | Departed | Cancelled

        public int flightDuration { get; set; } //user input
    }
}
