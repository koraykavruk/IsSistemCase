using MediatR;
using Reservation.Service.Events;
using Reservation.Service.Interfaces;

namespace Reservation.Service.Handlers
{
    internal class ReservationNotificationHandler : INotificationHandler<ReservationNotification>
    {
        private readonly IMailNotification _mailNotification;

        public ReservationNotificationHandler(IMailNotification mailNotification)
        {
            _mailNotification = mailNotification;
        }

        public Task Handle(ReservationNotification notification, CancellationToken cancellationToken)
        {
            var reservation = notification.Reservation;

            var mail = $"Sayın {reservation.CustomerName}, rezervasyonunuz başarıyla alındı. Masa No: {reservation.TableNumber}, Tarih: {reservation.ReservationDate}, Kişi Sayısı: {reservation.NumberOfGuests}";

            _mailNotification.SendMail(reservation.Mail, "Rezervasyon onayı", mail);

            reservation.SendMail = true;

            return Task.CompletedTask;
        }
    }
}