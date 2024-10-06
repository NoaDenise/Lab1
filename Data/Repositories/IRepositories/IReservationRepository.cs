using Lab1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab1.Data.Repositories.IRepositories
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetAllReservationsAsync();
        Task<Reservation> GetReservationByIdAsync(int id);
        Task<Reservation> AddReservationAsync(Reservation reservation);
        Task<Table> FindAvailableTableAsync(DateTime reservationDate, TimeSpan reservationTime, int numberOfGuests);
        Task<Reservation> UpdateReservationAsync(Reservation reservation);
        Task<Reservation> GetReservationByCustomerIdAsync(int customerId);
        Task DeleteReservationAsync(int id);
    }
}
