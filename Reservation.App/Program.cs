
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Reservation.Service;
using Reservation.Service.Events;
using Reservation.Service.Interfaces;

ServiceCollection serviceCollecton = new();

serviceCollecton.RegisterServices();

ServiceProvider serviceProvider = serviceCollecton.BuildServiceProvider();


var reservationService = serviceProvider.GetService<IReservation>();
var mediator = serviceProvider.GetService<IMediator>();


while (true)
{
    Console.Write("Rezervasyon i�in ki�i say�s�n� giriniz: ");

    var validNumber = int.TryParse(Console.ReadLine(), out int guestNumber);

    if (!validNumber)
    {
        Console.WriteLine("Ge�erli bir ki�i say�s� giriniz.");
        Console.WriteLine();
    }
    else
    {

        Console.Write("Rezarvasyon zaman�n� giriniz (�rnek: 2023-12-8 20:00) : ");

        var validDatetime = DateTime.TryParse(Console.ReadLine(), out DateTime reservationDate);

        if (!validDatetime)
        {
            Console.WriteLine("Ge�erli bir tarih giriniz.");
            Console.WriteLine();
        }
        else
        {
            var tables = reservationService.GetTables(reservationDate, guestNumber);

            if (!tables.Any())
            {
                Console.WriteLine("�zg�n�z, uygun masa bulunamad�.");
            }
            else
            {
                while(true)
                {
                    foreach (var table in tables)
                    {
                        Console.WriteLine("Masa No: {0}, Ki�i Say�s�: {1}", table.Number, table.Capacity);
                    }

                    Console.Write("Masa Se�iniz: ");

                    var validTable = int.TryParse(Console.ReadLine(), out int tableNumber) && tables.Exists(x => x.Number == tableNumber);

                    if (!validTable)
                    {
                        Console.WriteLine("Ge�erli bir masa numaras� giriniz.");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.Write("M��teri Ad�: ");

                        var customerName = Console.ReadLine();

                        Console.Write("Mail Adresi: ");
                        var mail = Console.ReadLine();

                        var reservation = new Reservation.Service.Models.ReservationModel(customerName, reservationDate, guestNumber, tableNumber, mail);
                        reservationService.SaveReservation(reservation);

                        _ = mediator.Publish(new ReservationNotification(reservation), new CancellationToken());

                        Console.WriteLine("Rezervasyon ba�ar�yla yap�lm��t�r. M��teri Ad�: {0}, Masa No: {1}, Tarih: {2}, Ki�i Say�s�: {3}", customerName, tableNumber, reservationDate, guestNumber);
                        Console.WriteLine();

                        break;
                    }
                }

                Console.Write("Yeni rezervasyon giri�i yapmak istermisiniz? (e/h): ");
        

                if (Console.ReadLine()?.ToLower() == "h")
                {
                    Environment.Exit(0);
                }

                Console.WriteLine();
            }
        }
    }
}

