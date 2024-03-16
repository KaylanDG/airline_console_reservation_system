
using System;
using System.Collections.Generic;

public static class OverviewLogic
{
    public static void PrintFlightOverview(List<Flight> flights)
    {
        if (flights == null)
        {
            Console.WriteLine("No flights available.");
            return;
        }

        Console.WriteLine("Flight Overview:");
        foreach (Flight flight in flights)
        {
            Console.WriteLine($"Flight {flight.FlightNumber} from {flight.From} to {flight.Destination}");
            Console.WriteLine($"Departure: {flight.DepartureDate} at {flight.DepartureTime}");
            Console.WriteLine($"Arrival: {flight.ArrivalDate} at {flight.ArrivalTime}");
            Console.WriteLine($"Duration: {flight.FlightDuration}");

            if (flight.Plane != null)
            {
                Console.WriteLine($"Airline: {flight.Plane.Airline}, Aircraft: {flight.Plane.Name}, Passengers: {flight.Plane.Passengers}");
            }
            else
            {
                Console.WriteLine("Plane information not available.");
            }

            Console.WriteLine();
        }
    }
}