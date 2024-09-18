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

        public ReservationController(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
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
                TableId = r.TableId,
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
                TableId = reservation.TableId,
                ReservationDate = reservation.ReservationDate,
                NumberOfGuests = reservation.NumberOfGuests
            };

            return Ok(reservationDTO);
        }

        // POST: api/Reservation
        [HttpPost]
        public async Task<ActionResult<ReservationDTO>> AddReservation(ReservationCreateDTO reservationCreateDTO)
        {
            // Enkel validering av input
            if (reservationCreateDTO.CustomerId <= 0 || reservationCreateDTO.TableId <= 0 || reservationCreateDTO.NumberOfGuests <= 0)
            {
                return BadRequest("Ogiltiga värden. Kund-ID, bords-ID och antal gäster måste vara större än noll."); // Returnerar 400 vid felaktiga värden
            }

            var reservation = new Reservation
            {
                CustomerId = reservationCreateDTO.CustomerId,
                TableId = reservationCreateDTO.TableId,
                ReservationDate = reservationCreateDTO.ReservationDate,
                NumberOfGuests = reservationCreateDTO.NumberOfGuests
            };

            var addedReservation = await _reservationRepository.AddReservationAsync(reservation);

            var reservationDTO = new ReservationDTO
            {
                ReservationId = addedReservation.ReservationId,
                CustomerId = addedReservation.CustomerId,
                TableId = addedReservation.TableId,
                ReservationDate = addedReservation.ReservationDate,
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
    }
}
