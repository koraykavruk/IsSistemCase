using MediatR;
using Reservation.Service.Events;
using Reservation.Service.Interfaces;
using Reservation.Service.Models;

namespace Reservation.Service.Service
{
    internal class ReservationService : IReservation
    {
        private readonly ContextModel _context;
        private readonly IMediator _mediator;

        public ReservationService(ContextModel context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public List<ReservationModel> GetReservations()
        {
            return _context.Reservations.ToList();
        }

        public List<TableModel> GetTables(DateTime reservationDate, int guestNumber)
        {
            var tables = _context.Tables.Where(x => x.Capacity >= guestNumber);

            var returnData = tables.Select(x =>
            {
                if (_context.Reservations != null)
                {
                    x.IsAvailable = !_context.Reservations.Exists(y => y.TableNumber == x.Number && reservationDate.Ticks <= y.ReservationDate.AddHours(2).Ticks);
                }

                return x;
            }).Where(x => x.IsAvailable).ToList();

            return returnData;
        }

        public void SaveReservation(ReservationModel reservation)
        {
            if (_context.Reservations == null)
                _context.Reservations = new List<ReservationModel>();

            _context.Reservations.Add(reservation);
        }
    }
}