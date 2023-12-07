using MediatR;
using Reservation.Service.Models;

namespace Reservation.Service.Events
{
    public class ReservationNotification : INotification
    {
        public ReservationNotification(ReservationModel reservation)
        {
            Reservation = reservation;
        }

        public ReservationModel Reservation { get; set; }
    }
}