using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Management_System03.Models
{
    public class Booking
    {
        public int bookingId { get; set; } //generated system
        public int passengerId { get; set; } //user input
        public int flightId { get; set; } //user input from the list
        public string seatNumber { get; set; } //generated system
        public string bookingDate { get; set; } //user input
        public decimal totalPrice { get; set; } //calculated
        public string status { get; set; }//default value,  Booking status: Confirmed | Cancelled
    }
}
