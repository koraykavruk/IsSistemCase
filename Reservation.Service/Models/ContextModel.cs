namespace Reservation.Service.Models
{
    internal class ContextModel
    {
        public List<TableModel> Tables = new List<TableModel>()
        {
            new (1,4, true),
            new (2,4, true),
            new (3,4, true),
            new (4,4, true),
            new (5,4, true),
            new (6,4, true),
            new (7,4, true),
            new (8,10, true),
            new (9,10, true),
            new (10,10, true)
        };

        public List<ReservationModel>? Reservations { get; set; }
    }
}