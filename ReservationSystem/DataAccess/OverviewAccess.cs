using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class OverviewAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/flights.json"));

    public static List<Flight> LoadAllFlights()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<Flight>>(json);
    }
}
