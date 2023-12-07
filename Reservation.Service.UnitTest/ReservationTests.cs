using Microsoft.Extensions.DependencyInjection;
using Reservation.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Service.UnitTest
{
    [TestFixture]
    public class ReservationTests
    {
        private IReservation _reservationService;
        
        [SetUp]
        public void Setup()
        {
            ServiceCollection serviceCollecton = new();

            serviceCollecton.RegisterServices();

            ServiceProvider serviceProvider = serviceCollecton.BuildServiceProvider();


            _reservationService = serviceProvider.GetService<IReservation>();
        }


        [Test]
        public void Bring_Tables_That_Can_Currently_Seat_10_People()
        {
            var tables = _reservationService.GetTables(DateTime.Now, 10);

            Assert.That(tables.Count, Is.EqualTo(3));
        }

        [Test]
        public void Bring_Tables_That_Can_Currently_Seat_5_People()
        {
            var tables = _reservationService.GetTables(DateTime.Now, 5);

            Assert.That(tables.Count, Is.EqualTo(3));
        }

        [Test]
        public void Bring_Tables_That_Can_Currently_Seat_4_People()
        {
            var tables = _reservationService.GetTables(DateTime.Now, 4);

            Assert.That(tables.Count, Is.EqualTo(10));
        }

        [Test]
        public void Bring_Tables_That_Can_Currently_Seat_11_People()
        {
            var tables = _reservationService.GetTables(DateTime.Now, 11);

            Assert.That(tables.Count, Is.EqualTo(0));
        }       
    }
}
