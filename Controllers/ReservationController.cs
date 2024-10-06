using Lab1.Data.Repositories.IRepositories;
using Lab1.Models;
using Lab1.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ICustomerRepository _customerRepository; // Lägg till denna rad

        public ReservationController(IReservationRepository reservationRepository, ICustomerRepository customerRepository) // Ändra konstruktorn
        {
            _reservationRepository = reservationRepository;
            _customerRepository = customerRepository;
        }

        // GET: api/Reservation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetAllReservations()
        {
            var reservations = await _reservationRepository.GetAllReservationsAsync();

            if (reservations == null || !reservations.Any())
            {
                return NotFound("Inga reservationer hittades."); // Returnerar 404 om inga reservationer finns
            }

            var reservationDTOs = reservations.Select(r => new ReservationDTO
            {
                ReservationId = r.ReservationId,
                CustomerId = r.CustomerId,
                TableId = r.TableId ?? 0,
                ReservationDate = r.ReservationDate,
                NumberOfGuests = r.NumberOfGuests
            });

            return Ok(reservationDTOs);
        }

        // GET: api/Reservation/{reservationId}
        [HttpGet("{reservationId}")]
        public async Task<ActionResult<ReservationDTO>> GetReservationById(int reservationId)
        {
            var reservation = await _reservationRepository.GetReservationByIdAsync(reservationId);
            if (reservation == null)
            {
                return NotFound($"Reservationen med ID {reservationId} hittades inte."); // Returnerar 404 om reservationen inte finns
            }

            var reservationDTO = new ReservationDTO
            {
                ReservationId = reservation.ReservationId,
                CustomerId = reservation.CustomerId,
                TableId = reservation.TableId ?? 0,
                ReservationDate = reservation.ReservationDate,
                NumberOfGuests = reservation.NumberOfGuests
            };

            return Ok(reservationDTO);
        }

        // POST: api/Reservation
        [HttpPost]
        public async Task<ActionResult<ReservationDTO>> AddReservation(ReservationCreateDTO reservationCreateDTO)
        {
            if (reservationCreateDTO.ReservationTime.Minutes != 0)
            {
                return BadRequest("Endast hela timmar kan bokas."); // Returnerar fel om bokningen inte är på en hel timme
            }

            // Kontrollera om kunden redan finns baserat på e-post
            var customer = await _customerRepository.GetCustomerByEmailAsync(reservationCreateDTO.Email);

            // Om kunden inte finns, skapa en ny kund
            if (customer == null)
            {
                customer = new Customer
                {
                    Name = reservationCreateDTO.Name,
                    Email = reservationCreateDTO.Email,
                    Phone = "Not Provided"  // Eller lämna som null
                };
                customer = await _customerRepository.AddCustomerAsync(customer);
            }

            // Hitta ett ledigt bord baserat på datum, tid och antal gäster
            var availableTable = await _reservationRepository.FindAvailableTableAsync(reservationCreateDTO.ReservationDate, reservationCreateDTO.ReservationTime, reservationCreateDTO.NumberOfGuests);

            if (availableTable == null)
            {
                return BadRequest("Inga tillgängliga bord för valt datum och tid.");
            }

            // Skapa bokningen med kundens ID och det lediga bordets ID
            var reservation = new Reservation
            {
                CustomerId = customer.CustomerId,
                ReservationDate = reservationCreateDTO.ReservationDate,
                ReservationTime = reservationCreateDTO.ReservationTime, // Lägg till tid för bokningen
                NumberOfGuests = reservationCreateDTO.NumberOfGuests,
                TableId = availableTable.TableId // Tilldela bordets ID här
            };

            var addedReservation = await _reservationRepository.AddReservationAsync(reservation);

            var reservationDTO = new ReservationDTO
            {
                ReservationId = addedReservation.ReservationId,
                CustomerId = addedReservation.CustomerId,
                TableId = addedReservation.TableId ?? 0,
                ReservationDate = addedReservation.ReservationDate,
                ReservationTime = addedReservation.ReservationTime, // Lägg till tid för bokningen
                NumberOfGuests = addedReservation.NumberOfGuests
            };

            return CreatedAtAction(nameof(GetReservationById), new { reservationId = addedReservation.ReservationId }, reservationDTO);
        }


        // PUT: api/Reservation/{reservationId}
        [HttpPut("{reservationId}")]
        public async Task<IActionResult> UpdateReservation(int reservationId, ReservationUpdateDTO reservationUpdateDTO)
        {
            if (reservationId != reservationUpdateDTO.ReservationId)
            {
                return BadRequest("Reservation-ID matchar inte."); // Kontrollera att ID:na matchar
            }

            var existingReservation = await _reservationRepository.GetReservationByIdAsync(reservationId);
            if (existingReservation == null)
            {
                return NotFound($"Reservationen med ID {reservationId} hittades inte."); // Returnerar 404 om reservationen inte finns
            }

            existingReservation.CustomerId = reservationUpdateDTO.CustomerId;
            existingReservation.TableId = reservationUpdateDTO.TableId;
            existingReservation.ReservationDate = reservationUpdateDTO.ReservationDate;
            existingReservation.NumberOfGuests = reservationUpdateDTO.NumberOfGuests;

            await _reservationRepository.UpdateReservationAsync(existingReservation);

            return NoContent(); // Returnerar 204 om uppdatering lyckas
        }

        // DELETE: api/Reservation/{reservationId}
        [HttpDelete("{reservationId}")]
        public async Task<IActionResult> DeleteReservation(int reservationId)
        {
            var existingReservation = await _reservationRepository.GetReservationByIdAsync(reservationId);
            if (existingReservation == null)
            {
                return NotFound($"Reservationen med ID {reservationId} hittades inte."); // Returnerar 404 om reservationen inte finns
            }

            await _reservationRepository.DeleteReservationAsync(reservationId);
            return NoContent(); // Returnerar 204 efter borttagning
        }

        // DELETE: api/Reservation/deleteByEmail/{email}
        [HttpDelete("deleteByEmail/{email}")]
        public async Task<IActionResult> DeleteReservationByEmail(string email)
        {
            // Hämta kund baserat på e-post
            var customer = await _customerRepository.GetCustomerByEmailAsync(email);

            if (customer == null)
            {
                return NotFound($"Ingen kund med e-postadressen {email} hittades.");
            }

            // Hämta bokning baserat på kundens ID
            var reservation = await _reservationRepository.GetReservationByCustomerIdAsync(customer.CustomerId);

            if (reservation == null)
            {
                return NotFound($"Ingen bokning hittades för kunden med e-post {email}.");
            }

            // Ta bort bokningen
            await _reservationRepository.DeleteReservationAsync(reservation.ReservationId);
            return NoContent();
        }

    }
}
