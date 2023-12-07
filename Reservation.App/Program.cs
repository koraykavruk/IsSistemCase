
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
    Console.Write("Rezervasyon için kiþi sayýsýný giriniz: ");

    var validNumber = int.TryParse(Console.ReadLine(), out int guestNumber);

    if (!validNumber)
    {
        Console.WriteLine("Geçerli bir kiþi sayýsý giriniz.");
        Console.WriteLine();
    }
    else
    {

        Console.Write("Rezarvasyon zamanýný giriniz (Örnek: 2023-12-8 20:00) : ");

        var validDatetime = DateTime.TryParse(Console.ReadLine(), out DateTime reservationDate);

        if (!validDatetime)
        {
            Console.WriteLine("Geçerli bir tarih giriniz.");
            Console.WriteLine();
        }
        else
        {
            var tables = reservationService.GetTables(reservationDate, guestNumber);

            if (!tables.Any())
            {
                Console.WriteLine("Üzgünüz, uygun masa bulunamadý.");
            }
            else
            {
                while(true)
                {
                    foreach (var table in tables)
                    {
                        Console.WriteLine("Masa No: {0}, Kiþi Sayýsý: {1}", table.Number, table.Capacity);
                    }

                    Console.Write("Masa Seçiniz: ");

                    var validTable = int.TryParse(Console.ReadLine(), out int tableNumber) && tables.Exists(x => x.Number == tableNumber);

                    if (!validTable)
                    {
                        Console.WriteLine("Geçerli bir masa numarasý giriniz.");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.Write("Müþteri Adý: ");

                        var customerName = Console.ReadLine();

                        Console.Write("Mail Adresi: ");
                        var mail = Console.ReadLine();

                        var reservation = new Reservation.Service.Models.ReservationModel(customerName, reservationDate, guestNumber, tableNumber, mail);
                        reservationService.SaveReservation(reservation);

                        _ = mediator.Publish(new ReservationNotification(reservation), new CancellationToken());

                        Console.WriteLine("Rezervasyon baþarýyla yapýlmýþtýr. Müþteri Adý: {0}, Masa No: {1}, Tarih: {2}, Kiþi Sayýsý: {3}", customerName, tableNumber, reservationDate, guestNumber);
                        Console.WriteLine();

                        break;
                    }
                }

                Console.Write("Yeni rezervasyon giriþi yapmak istermisiniz? (e/h): ");
        

                if (Console.ReadLine()?.ToLower() == "h")
                {
                    Environment.Exit(0);
                }

                Console.WriteLine();
            }
        }
    }
}

