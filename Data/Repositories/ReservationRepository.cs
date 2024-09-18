using Lab1.Data.Repositories.IRepositories;
using Lab1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab1.Data.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly Lab1DbContext _context;

        public ReservationRepository(Lab1DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
        {
            try
            {
                // Hämtar alla reservationer från databasen
                return await _context.Reservations
                    .Include(r => r.Customer)
                    .Include(r => r.Table)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ett fel uppstod när reservationerna hämtades.", ex);
            }
        }

        public async Task<Reservation> GetReservationByIdAsync(int id)
        {
            try
            {
                // Hämtar en specifik reservation baserat på ID
                return await _context.Reservations
                    .Include(r => r.Customer)
                    .Include(r => r.Table)
                    .FirstOrDefaultAsync(r => r.ReservationId == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ett fel uppstod när reservationen med ID {id} hämtades.", ex);
            }
        }

        public async Task<Reservation> AddReservationAsync(Reservation reservation)
        {
            try
            {
                // Lägger till en ny reservation i databasen
                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();
                return reservation;
            }
            catch (Exception ex)
            {
                throw new Exception("Ett fel uppstod när reservationen skulle sparas i databasen.", ex);
            }
        }

        public async Task<Reservation> UpdateReservationAsync(Reservation reservation)
        {
            try
            {
                // Uppdaterar en befintlig reservation
                var existingReservation = await _context.Reservations.FindAsync(reservation.ReservationId);
                if (existingReservation != null)
                {
                    existingReservation.CustomerId = reservation.CustomerId;
                    existingReservation.TableId = reservation.TableId;
                    existingReservation.ReservationDate = reservation.ReservationDate;
                    existingReservation.NumberOfGuests = reservation.NumberOfGuests;
                    _context.Entry(existingReservation).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return existingReservation;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ett fel uppstod när reservationen med ID {reservation.ReservationId} skulle uppdateras.", ex);
            }
        }

        public async Task DeleteReservationAsync(int id)
        {
            try
            {
                // Tar bort en reservation baserat på ID
                var reservation = await _context.Reservations.FindAsync(id);
                if (reservation != null)
                {
                    _context.Reservations.Remove(reservation);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ett fel uppstod när reservationen med ID {id} skulle raderas.", ex);
            }
        }
    }
}
