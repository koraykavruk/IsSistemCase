namespace Reservation.Service.Models
{
    public class ReservationModel
    {
        public ReservationModel(string customerName, DateTime reservationDate, int numberOfGuests, int tableNumber, string mail)
        {
            CustomerName = customerName;
            ReservationDate = reservationDate;
            NumberOfGuests = numberOfGuests;
            TableNumber = tableNumber;
            Mail = mail;
        }

        public string CustomerName { get; set; }
        public string Mail { get; set; }
        public DateTime ReservationDate { get; set; }
        public int NumberOfGuests { get; set; }
        public int TableNumber { get; set; }

        public bool SendMail { get; set; }
    }
}