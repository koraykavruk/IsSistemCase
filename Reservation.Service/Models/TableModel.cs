namespace Reservation.Service.Models
{
    public class TableModel
    {
        public TableModel(int number, int capacity, bool isAvailable)
        {
            Number = number;
            Capacity = capacity;
            IsAvailable = isAvailable;
        }

        public int Number { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }
    }
}