using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class ReservationAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/reservations.json"));

    public static List<ReservationModel> LoadAll()
    {
        if (!File.Exists(path))
            return new List<ReservationModel>();
        string json = File.ReadAllText(path);
        var options = new JsonSerializerOptions();
        options.Converters.Add(new CustomDateTimeConverter()); // Register custom converter
        return JsonSerializer.Deserialize<List<ReservationModel>>(json, options);
    }

    public void WriteAll(List<ReservationModel> reservations)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(reservations, options);
        File.WriteAllText(path, json);
    }

    public void AddReservation(ReservationModel reservation)
    {
        var reservations = LoadAll();
        reservations.Add(reservation);
        WriteAll(reservations);
    }

    // Method to retrieve reservations by reservation code
    public List<ReservationModel> GetReservationsByCode(string reservationCode)
    {
        var reservations = LoadAll();
        return reservations.Where(r => r.ReservationCode.Equals(reservationCode, StringComparison.OrdinalIgnoreCase)).ToList();
    }

}
