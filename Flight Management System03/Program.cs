using Flight_Management_System03.Models;
using System.Numerics;
using System.Security.Cryptography;
using System.Threading.Channels;

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
    
            //display all available operational Aircraft --choose from it 

            List<Aircraft> choosenAircraft = context.Aircrafts.Where(a => a.isOperational == true)
                                                          .ToList();
            foreach(Aircraft a in choosenAircraft)
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
            List<Pilot> choosenPilot = context.Pilots.Where(a => a.isAvailable == true)
                                              .ToList();
            foreach (Pilot p in choosenPilot)
            {
                Console.WriteLine("pilotId: "+p.pilotId+ " | pilotName : "+p.pilotName);
            }


            Console.WriteLine("Enter assigned  pilotId for the flight");
            int choosenPilotId = int.Parse(Console.ReadLine());

            Pilot choicePilot = context.Pilots.FirstOrDefault(a => a.pilotId == choosenPilotId);

            if (choicePilot == null)
            {
                Console.WriteLine("invalied pilot id");
                return;
            }
            choicePilot.isAvailable = false;

            // create the flight record

            int flightId = context.Flights.Count + 1;

            string flightCode = "OA"+ (context.Flights.Count + 1);


            Console.WriteLine("Enter  Departure airport / city");
            string origin = Console.ReadLine();

            Console.WriteLine("Enter  Arrival airport / city");
            string destination = Console.ReadLine();

            Console.WriteLine("Enter  departure Date");
            string departureDate = Console.ReadLine();

            Console.WriteLine("Enter  departure Time");
            string departureTime = Console.ReadLine();

            Console.WriteLine("Enter  ticket Price");
            decimal ticketPrice = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Enter  flight Duration");
            int flightDuration = int.Parse(Console.ReadLine());

            Aircraft seats = context.Aircrafts.FirstOrDefault(t => t.aircraftId == choosenAircraftId);
            if (seats == null)
            {
                Console.WriteLine("No available seats");
                return;
            }
            context.Flights.Add(new Flight
            {
                flightId= flightId,
                flightCode= flightCode,
                aircraftId= choosenAircraftId,
                pilotId= choosenPilotId,
                origin = origin,
                destination = destination,
                departureDate= departureDate,
                departureTime= departureTime,
                ticketPrice= ticketPrice,
                flightDuration= flightDuration,
                availableSeats= seats.totalSeats,
                status= "Scheduled"
            });
            
            Console.WriteLine("The flight added successfully  , with ID: " + flightId);

        }

        //06 Book a Flight
        public static void BookFlight()
        {
            Console.WriteLine("Enter passenger ID");
            int passengerId = int.Parse(Console.ReadLine());

            Passenger selectPassenger = context.Passengers.FirstOrDefault(a => a.passengerId == passengerId);

            if (selectPassenger == null)
            {
                Console.WriteLine("Passenger not found");
                return;
            }

            //choose a destination
            Console.WriteLine("Enter the Destinations");
            string enterDestination = Console.ReadLine();

            List<Flight> availabeDestinations = context.Flights.Where(a => a.status == "Scheduled" 
                                                                      && a.destination == enterDestination 
                                                                      && a.availableSeats > 0)
                                                               .ToList();

            if (availabeDestinations.Count == 0)
            {
                Console.WriteLine("Destinations not found or seats not available");
                return;
            }

            Console.WriteLine("===Availabe Destinations===");
            foreach (Flight f in availabeDestinations)
            {
                Console.WriteLine("flightId: "+ f.flightId + "|destination: "+f.destination+ "|availableSeats: "+f.availableSeats); 
            }

            //flightId that user want to book 
            Console.WriteLine("Enter the flightId that you want to booked");
            int bookedFlightId = int.Parse(Console.ReadLine());

            Flight selectedFlight = context.Flights.FirstOrDefault(a => a.flightId == bookedFlightId);

            if (selectedFlight == null)
            {
                Console.WriteLine("flight not found");
                return;
            }

            //create booking
            int bookingId = context.Bookings.Count + 1;

            string seatNumber = "A" + (context.Bookings.Count + 1);

            Console.WriteLine("Enter booking Date");
            string bookingDate = Console.ReadLine();

            context.Bookings.Add(new Booking
            {
                bookingId= bookingId,
                passengerId= passengerId,
                flightId= bookedFlightId,
                seatNumber= seatNumber,
                bookingDate= bookingDate,
                totalPrice= selectedFlight.ticketPrice,
                status= "Confirmed"

            });
            selectedFlight.availableSeats = selectedFlight.availableSeats - 1;

            Console.WriteLine("The booking added successfully  , with ID: " + bookingId);

        }

        //07 Cancel a Booking
        public static void CancelBooking()
        {
            //
            Console.WriteLine("Enter passenger ID");
            int passengerId = int.Parse(Console.ReadLine());

            Passenger selectPassenger = context.Passengers.FirstOrDefault(a => a.passengerId == passengerId);

            if (selectPassenger == null)
            {
                Console.WriteLine("Passenger not found");
                return;
            }

            // The booking is located
            Console.WriteLine("Enter bookingId that you would cancelled");
            int bookingId = int.Parse(Console.ReadLine());

            Booking selectedBooking = context.Bookings.FirstOrDefault(a => a.bookingId == bookingId);

            if (selectedBooking == null)
            {
                Console.WriteLine("Booking not found");
                return;
            }

            //status is set to Cancelled,
            selectedBooking.status = "Cancelled";

            //the seat is returned to the flight so another passenger can book it.

            Console.WriteLine("Enter flightId that you would cancelled");
            int flightId = int.Parse(Console.ReadLine());

            Flight selectedFlight = context.Flights.FirstOrDefault(a => a.flightId == flightId );

            if (selectedFlight == null)
            {
                Console.WriteLine("Flight not found");
                return;
            }

            selectedFlight.availableSeats = selectedFlight.availableSeats + 1;

            Console.WriteLine("The booking cancelled successfully" );

        }

        //08 Depart a Flight

        public static void DepartFlight()
        {
            //display all scheduled flights
            List<Flight> scheduledFlights = context.Flights.Where(a => a.status == "Scheduled").ToList();

            if (scheduledFlights.Count == 0)
            {
                Console.WriteLine("There is no scheduled Flights");
                return;
            }
            foreach (Flight f in scheduledFlights)
            {
                Console.WriteLine("flightId: "+ f.flightId);
                Console.WriteLine("flightCode: " + f.flightCode);
            }

            //enter flight id from the  that will change the status to Departed
            Console.WriteLine("Enter flight Id to chance ststus");
            int enteredFlightId = int.Parse(Console.ReadLine());

            Flight selectededFlight = context.Flights.FirstOrDefault(d => d.flightId == enteredFlightId);

            if (selectededFlight == null)
            {
                Console.WriteLine("invaled Flight Id");
                return;
            }

            //coniform that selected pilot is available and assign

            Console.WriteLine("Enter Pilot Id");
            int enteredPilotId = int.Parse(Console.ReadLine());

            Pilot selectedPilot = context.Pilots.FirstOrDefault(x => x.pilotId == enteredPilotId && x.isAvailable==true);

            if (selectedPilot == null)
            {
                Console.WriteLine("invaled Pilot Id");
                return;
            }

            //pilot's total flight hours
            selectedPilot.flightHours = selectedPilot.flightHours + selectededFlight.flightDuration;

            //change the status
            selectededFlight.status = "Departed";

            Console.WriteLine("Flight departed successfully and pilot's total flight hours are updated");


        }

        //09 Cancel a Flight
        public static void CancelFlight()
        {
            //display the flights the user choose the flight id that he want to cancel flight 

            List<Flight> diplayFlights = context.Flights.Where(x => x.status == "Scheduled").ToList();
            if (diplayFlights.Count == 0)
            {
                Console.WriteLine("No flights has been scheduled yet");
                return;
            }

            Console.WriteLine("Enter the flight ID that you want to cancel");
            int enteredFlightId = int.Parse(Console.ReadLine());

            Flight selectedFlightId = context.Flights.FirstOrDefault(x => x.flightId == enteredFlightId && x.status== "Scheduled");

            if (selectedFlightId == null)
            {
                Console.WriteLine("invalied Flight Id");
                return;
            }
            //change the status 
            selectedFlightId.status = "Cancelled";

            //change all assosiated bookings to cancelled
            List<Booking> bookingList = context.Bookings.Where(b => b.status == "Confirmed" && b.flightId == enteredFlightId).ToList();
            if (bookingList.Count == 0)
            {
                Console.WriteLine("No assosiated bookings reated to this flight id");
                return;
            }
            //change the status for each booking in the list
            foreach(Booking b in bookingList)
            {
                b.status = "Cancelled";
                selectedFlightId.availableSeats++;


            }
            Console.WriteLine("All bookings are cancelled successfully");

            // pilot that it is assosiated to that flight
            Pilot selectedPilot = context.Pilots.FirstOrDefault(p => p.pilotId == selectedFlightId.pilotId);

            if (selectedPilot == null)
            {
                Console.WriteLine("no pilot assosiated to that flight");
                return;
            }
            selectedPilot.isAvailable = true;
            Console.WriteLine("the pilot is available again");


            //total
            int affectedBookingsCount = bookingList.Count();
            Console.WriteLine("Total affected Bookings = "+ affectedBookingsCount);


        }

        //10 Passenger Booking History
        public static void PassengerBookingHistory()
        {
            Console.WriteLine("Enter passenger ID that you want to see Booking History");
            int enterPassengerId = int.Parse(Console.ReadLine());

            Passenger selectedPassenger = context.Passengers.FirstOrDefault(p => p.passengerId == enterPassengerId);
            if (selectedPassenger == null)
            {
                Console.WriteLine("Invalid passenger ID");
                return;
            }

            List<Booking> displayBookings = context.Bookings.Where(b => b.passengerId == enterPassengerId).ToList();
            if (displayBookings.Count == 0)
            {
                Console.WriteLine("No bookings found for this passenger.");
                return;
            }

            decimal total = 0;

            foreach (var booking in displayBookings)
            {
                Flight flight = context.Flights
                    .FirstOrDefault(f => f.flightId == booking.flightId);

                if (flight != null)
                {
                    Console.WriteLine("Flight Code: " + flight.flightCode);
                    Console.WriteLine("Origin: " + flight.origin);
                    Console.WriteLine("Destination: " + flight.destination);
                    Console.WriteLine("Departure Date: " + flight.departureDate);
                    Console.WriteLine("Seat Number: " + booking.seatNumber);
                    Console.WriteLine("Price Paid: " + booking.totalPrice);
                    Console.WriteLine("Booking Status: " + booking.status);
                    
                }

                if (booking.status == "Confirmed")
                {
                    total += booking.totalPrice;
                }
            }

            Console.WriteLine("Total Amount Spent (Confirmed only): " + total);



        }

        //11 Flight Revenue &Load Factor Report
        public static void FlightReport()
        {
            var report = context.Flights.Select()




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
                    case 01: RegisterPassenger();  break;

                    case 02: AddAircraft(); break;

                    case 03: RegisterPilot(); break;

                    case 04: ViewAllFlight(); break;

                    case 05: SchedulFlight();  break;

                    case 06: BookFlight();  break;

                    case 07: CancelBooking(); break;

                    case 08: DepartFlight();  break;

                    case 09: CancelFlight(); break;

                    case 10: PassengerBookingHistory(); break;

                    case 11: FlightReport(); break;

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
