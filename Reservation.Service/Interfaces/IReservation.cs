using Reservation.Service.Models;

namespace Reservation.Service.Interfaces
{
    public interface IReservation
    {
        void SaveReservation(ReservationModel reservation);

        List<TableModel> GetTables(DateTime reservationDate, int guestNumber);

        List<ReservationModel> GetReservations();
    }
}