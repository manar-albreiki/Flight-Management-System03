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
            string enteredPassportNumber = Console.ReadLine();
            bool unique = context.Passengers.Any(a => a.passportNumber == enteredPassportNumber);
            if (unique==true)
            {
                Console.WriteLine("The passport Number already used, please try again ");
                return;
            }

            Console.WriteLine("Enter your nationality ");
            string nationality = Console.ReadLine();

            context.Passengers.Add(new Passenger
            {
                passengerId=passengerId,
                passengerName= passengerName,
                passengerEmail= passengerEmail,
                passengerPhone= passengerPhone,
                passportNumber= enteredPassportNumber,
                nationality= nationality
            });
            Console.WriteLine($"Passenger registered successfully, with ID: {passengerId}");
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
            Console.WriteLine($"Aircrafts added successfully to the system, with ID: {aircraftId}");
        }

        //03 Register a Pilot
        public static void RegisterPilot()
        {
            int pilotId = context.Pilots.Count + 1;

            Console.WriteLine("Enter pilot Name");
            string pilotName = Console.ReadLine();

            Console.WriteLine("Enter pilot Phone");
            string pilotPhone = Console.ReadLine();

            Console.WriteLine("Enter license Number");
            string licenseNumber = Console.ReadLine();

            context.Pilots.Add(new Pilot
            {
                pilotId= pilotId,
                pilotName= pilotName,
                pilotPhone= pilotPhone,
                licenseNumber= licenseNumber,
                flightHours=0,
                isAvailable=true
            });
            Console.WriteLine($"pilot registered successfully, with ID: {pilotId}");
        }

        //04 View All Flights
        public static void ViewAllFlight()
        {
            Console.WriteLine("===== All scheduled flight =====");

            foreach (Flight f in context.Flights)
            {
                Console.WriteLine("flight code : " + f.flightCode);
                Console.WriteLine("origin : " + f.origin);
                Console.WriteLine("destination : " + f.destination);
                Console.WriteLine("departure date : " + f.departureDate);
                Console.WriteLine("departure time : " + f.departureTime);
                Console.WriteLine("available seats : " + f.availableSeats);
                Console.WriteLine("ticket price : " + f.ticketPrice);
                Console.WriteLine("current status : " + f.status);
            }
        }

        //05 Schedule a Flight
        public static void SchedulFlight()
        {
            //generate flight id
            int flightId = context.Flights.Count + 1;

            //display all available operational Aircraft --choose from it 

            List<bool> choosenAircraft = context.Aircrafts.Select(a => a.isOperational == true)
                                                          .ToList();
            foreach(Aircraft a in context.Aircrafts)
            {
                Console.WriteLine("aircraftId : " + a.aircraftId);
                Console.WriteLine("aircraft model : " + a.model);
                Console.WriteLine("aircraft totalSeats : " + a.totalSeats);
            }

            Console.WriteLine("Enter ID for choosen aircraft");
            int choosenAircraftId = int.Parse(Console.ReadLine());

            bool choiceAircraft = context.Aircrafts.Any(a => a.aircraftId == choosenAircraftId);
            if (choiceAircraft == false)
            {
                Console.WriteLine("no aircraft found with id");
                return;
            }

            //display all available pilot --choose from it
            List<bool> choosenPilot = context.Pilots.Select(a => a.isAvailable == true)
                                              .ToList();
            foreach (Pilot p in context.Pilots)
            {
                Console.WriteLine("pilotId: "+p.pilotId+ " | pilotName : "+p.pilotName);
            }


            Console.WriteLine("Enter assigned  pilotId for the flight");
            int choosenPilotId = int.Parse(Console.ReadLine());

            bool choicePilot = context.Aircrafts.Any(a => a.aircraftId == choosenAircraftId);
            if (choicePilot == false)
            {
                Console.WriteLine("no pilot found with id");
                return;
            }

            // create the flight record
         



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
