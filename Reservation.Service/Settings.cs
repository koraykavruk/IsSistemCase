using Microsoft.Extensions.DependencyInjection;
using Reservation.Service.Interfaces;
using Reservation.Service.Models;
using Reservation.Service.Service;
using System.Reflection;

namespace Reservation.Service
{
    public static class Settings
    {
        public static void RegisterServices(this IServiceCollection servicesBuilder)
        {
            servicesBuilder.AddSingleton<ContextModel>();
            servicesBuilder.AddSingleton<IReservation, ReservationService>();
            servicesBuilder.AddSingleton<IMailNotification, MailService>();
            servicesBuilder.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        }
    }
}