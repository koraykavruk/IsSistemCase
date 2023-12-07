using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Reservation.Service.Events;
using Reservation.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Service.UnitTest
{
    [TestFixture]
    public class ReservationIntegrationTest
    {
        private IReservation _reservationService;
        private IMediator _mediator;

        [SetUp]
        public void Setup()
        {
            ServiceCollection serviceCollecton = new();

            serviceCollecton.RegisterServices();

            ServiceProvider serviceProvider = serviceCollecton.BuildServiceProvider();


            _reservationService = serviceProvider.GetService<IReservation>();
            _mediator = serviceProvider.GetService<IMediator>();
        }

        [Test]
        public void Reservation_Send_Mail()
        {
           var reservation =  new Models.ReservationModel("test", DateTime.Now, 4, 1, "test@test.com");

            _reservationService.SaveReservation(reservation);

            _ = _mediator.Publish(new ReservationNotification(reservation), new CancellationToken());

            var testReservation = _reservationService.GetReservations().Find(x=> x.CustomerName == "test");

            Assert.That(testReservation.SendMail, Is.True);
        }

        [Test]
        public void Save_Reservation()
        {
            _reservationService.SaveReservation(new Models.ReservationModel("test", DateTime.Now, 4, 1, "test@test.com"));

            var reservations = _reservationService.GetReservations();

            Assert.That(reservations.Exists(y => y.CustomerName == "test"), Is.True);
        }

        [Test]
        public void No_Suitable_Table_Found()
        {
            _reservationService.SaveReservation(new Models.ReservationModel("test", DateTime.Now, 10, 8, "test@test.com"));
            _reservationService.SaveReservation(new Models.ReservationModel("test", DateTime.Now, 10, 9, "test@test.com"));
            _reservationService.SaveReservation(new Models.ReservationModel("test", DateTime.Now, 10, 10, "test@test.com"));

            var tables = _reservationService.GetTables(DateTime.Now, 10);

            Assert.That(tables.Count, Is.EqualTo(0));
        }


        [Test]
        public void Booked_Table_Number_8_Should_Not_Appear_On_The_Table_List()
        {
            _reservationService.SaveReservation(new Models.ReservationModel("test", DateTime.Now, 10, 8, "test@test.com"));

            var tables = _reservationService.GetTables(DateTime.Now, 10);

            Assert.That(tables.Exists(x => x.Number == 8), Is.False);
        }
    }
}