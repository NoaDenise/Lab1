using Lab1.Data.Repositories.IRepositories;
using Lab1.Models;
using Lab1.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TableController : ControllerBase
    {
        private readonly ITableRepository _tableRepository;

        public TableController(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TableDTO>>> GetAllTables()
        {
            var tables = await _tableRepository.GetAllTablesAsync();

            if (tables == null || !tables.Any())
            {
                return NotFound("Inga bord hittades.");  // Returnerar ett felmeddelande om inga bord finns
            }

            var tableDTOs = tables.Select(t => new TableDTO
            {
                TableId = t.TableId,
                TableNumber = t.TableNumber,
                NumberOfSeats = t.Seats
            });

            return Ok(tableDTOs);
        }

        [HttpGet("{tableId}")]
        public async Task<ActionResult<TableDTO>> GetTableById(int tableId)
        {
            var table = await _tableRepository.GetTableByIdAsync(tableId);
            if (table == null)
            {
                return NotFound($"Bordet med ID {tableId} hittades inte.");  // Returnerar 404 om bordet inte hittas
            }

            var tableDTO = new TableDTO
            {
                TableId = table.TableId,
                TableNumber = table.TableNumber,
                NumberOfSeats = table.Seats
            };

            return Ok(tableDTO);
        }

        [HttpPost]
        public async Task<ActionResult<TableDTO>> AddTable(CreateTableDTO tableDTO)
        {
            // Validering av bordets nummer och antal sittplatser
            if (tableDTO.TableNumber <= 0 || tableDTO.NumberOfSeats <= 0)
            {
                return BadRequest("Bordsnummer och antal sittplatser måste vara större än noll.");
            }

            var table = new Table
            {
                TableNumber = tableDTO.TableNumber,
                Seats = tableDTO.NumberOfSeats
            };

            try
            {
                var addedTable = await _tableRepository.AddTableAsync(table);

                var resultDTO = new TableDTO
                {
                    TableId = addedTable.TableId, // TableId genereras automatiskt
                    TableNumber = addedTable.TableNumber,
                    NumberOfSeats = addedTable.Seats
                };

                return CreatedAtAction(nameof(GetTableById), new { tableId = addedTable.TableId }, resultDTO);
            }
            catch (DbUpdateException)
            {
                return Conflict("Bordsnumret är redan upptaget. Ange ett annat nummer.");
            }
        }




        [HttpPut("{tableId}")]
        public async Task<IActionResult> UpdateTable(int tableId, TableDTO tableDTO)
        {
            if (tableId != tableDTO.TableId)
            {
                return BadRequest("Bords-ID matchar inte.");  
            }

            var existingTable = await _tableRepository.GetTableByIdAsync(tableId);
            if (existingTable == null)
            {
                return NotFound($"Bordet med ID {tableId} hittades inte.");  // Returnerar 404 om bordet inte hittas
            }

            existingTable.TableNumber = tableDTO.TableNumber;
            existingTable.Seats = tableDTO.NumberOfSeats;

            var updatedTable = await _tableRepository.UpdateTableAsync(existingTable);
            if (updatedTable == null)
            {
                return NotFound();  // Returnerar 404 om bordet inte kunde uppdateras
            }

            return NoContent();  // Returnerar 204 om uppdateringen lyckades
        }

        [HttpDelete("{tableId}")]
        public async Task<IActionResult> DeleteTable(int tableId)
        {
            var existingTable = await _tableRepository.GetTableByIdAsync(tableId);
            if (existingTable == null)
            {
                return NotFound($"Bordet med ID {tableId} hittades inte.");  // Returnerar 404 om bordet inte hittas
            }

            await _tableRepository.DeleteTableAsync(tableId);
            return NoContent();  // Returnerar 204 efter borttagning
        }
    }
}
