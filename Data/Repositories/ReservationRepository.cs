using Lab1.Data.Repositories.IRepositories;
using Lab1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq; // Lägg till detta för att använda LINQ

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
                var reservations = await _context.Reservations
                    .Include(r => r.Customer)
                    .Include(r => r.Table)
                    .ToListAsync();

                // Lägg till denna rad för att logga antalet hämtade reservationer
                Console.WriteLine($"Antal reservationer hämtade: {reservations.Count}");

                return reservations;
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

        // Här implementerar vi den saknade metoden
        public async Task<Table> FindAvailableTableAsync(DateTime reservationDate, TimeSpan reservationTime, int numberOfGuests)
        {
            try
            {
                // Hitta ett bord som har tillräckligt många platser och som inte är bokat vid samma datum och tid
                var availableTable = await _context.Tables
                    .Where(t => t.Seats >= numberOfGuests &&
                                !_context.Reservations
                                    .Any(r => r.TableId == t.TableId &&
                                              r.ReservationDate.Date == reservationDate.Date && // Kontrollera datum
                                              r.ReservationTime == reservationTime)) // Kontrollera tid
                    .FirstOrDefaultAsync();

                return availableTable;
            }
            catch (Exception ex)
            {
                throw new Exception("Ett fel uppstod när ett ledigt bord skulle hittas.", ex);
            }
        }

        public async Task<Reservation> GetReservationByCustomerIdAsync(int customerId)
        {
            try
            {
                return await _context.Reservations
                    .FirstOrDefaultAsync(r => r.CustomerId == customerId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ett fel uppstod när bokningen för kunden med ID {customerId} skulle hämtas.", ex);
            }
        }

    }
}
