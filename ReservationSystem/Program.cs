// See https://aka.ms/new-console-template for more information
Console.Clear();

Console.WriteLine("\n██     ██ ███████ ██       ██████  ██████  ███    ███ ███████");
Console.WriteLine("██     ██ ██      ██      ██      ██    ██ ████  ████ ██     ");
Console.WriteLine("██  █  ██ █████   ██      ██      ██    ██ ██ ████ ██ █████  ");
Console.WriteLine("██ ███ ██ ██      ██      ██      ██    ██ ██  ██  ██ ██     ");
Console.WriteLine(" ███ ███  ███████ ███████  ██████  ██████  ██      ██ ███████\n");


ReservationLogic _reservationLogic = new ReservationLogic();
Console.WriteLine(_reservationLogic.ExtraLuggage(2));



