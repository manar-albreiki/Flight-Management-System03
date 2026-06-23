using Flight_Management_System03.Models;

namespace Flight_Management_System03
{
    internal class Program
    {
        public static FlightContext context = new FlightContext
        {
            Passengers = new List<Passenger>(),
            Pilots = new List<Pilot>(),
            Aircrafts = new List<Aircraft>(),
            Flights = new List<Flight>(),
            Bookings = new List<Booking>()
        };

        //01 Register a Passenger
        public static void RegisterPassenger()
        {
            int passengerId = context.Passengers.Count + 1;

            Console.WriteLine("Enter passenger Name ");
            string passengerName = Console.ReadLine();

            Console.WriteLine("Enter passenger Email ");
            string passengerEmail = Console.ReadLine();

            Console.WriteLine("Enter passenger Phone ");
            string passengerPhone = Console.ReadLine();

            Console.WriteLine("Enter passport Number ");
            string passportNumber = Console.ReadLine();

            Console.WriteLine("Enter your nationality ");
            string nationality = Console.ReadLine();

            context.Passengers.Add(new Passenger
            {
                passengerId=passengerId,
                passengerName= passengerName,
                passengerEmail= passengerEmail,
                passengerPhone= passengerPhone,
                passportNumber= passportNumber,
                nationality= nationality
            });
            Console.WriteLine($"Passenger registered successfully. Assigned ID: {passengerId}");
        }

        //02 Add an Aircraft
        public static void AddAircraft()
        {
            int aircraftId = context.Aircrafts.Count + 1;

            Console.WriteLine("Enter model of Aircrafts");
            string model = Console.ReadLine();

            Console.WriteLine("Enter totalSeats of Aircrafts");
            int totalSeats= int.Parse(Console.ReadLine());

            context.Aircrafts.Add(new Aircraft
            {
                aircraftId= aircraftId,
                model= model,
                totalSeats= totalSeats,
                isOperational=true
            });
            Console.WriteLine($"Aircrafts added successfully to the system . Assigned ID: {aircraftId}");
        }


        static void Main(string[] args)
        {
            bool exit = false;
            while (exit == false)
            {
                Console.WriteLine("======= Welcome to Flight Management System =======");
                Console.WriteLine("01  Register a Passenger");
                Console.WriteLine("02  Add an Aircraft");
                Console.WriteLine("03  Register a Pilot");
                Console.WriteLine("04 View All Flights");
                Console.WriteLine("05  Schedule a Flight");
                Console.WriteLine("06  Book a Flight");
                Console.WriteLine("07 Cancel a Booking");
                Console.WriteLine("08 Depart a Flight");
                Console.WriteLine("09 Cancel a Flight");
                Console.WriteLine("10 Passenger Booking History");
                Console.WriteLine("11 Flight Revenue & Load Factor Report");
                Console.WriteLine("0 Exit");



                Console.WriteLine("Please enter an option :");
                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 01:  break;

                    case 02:  break;

                    case 03:  break;

                    case 04:  break;

                    case 05:  break;

                    case 06:  break;

                    case 07:  break;

                    case 08:  break;

                    case 09:  break;

                    case 10:  break;

                    case 11: break;

                    case 0: exit = true; break;

                    default: Console.WriteLine("Invalid option. Please try again."); break;


                }
                if (!exit)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
    }
}
