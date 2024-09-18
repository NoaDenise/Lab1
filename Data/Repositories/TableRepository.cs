using Lab1.Data.Repositories.IRepositories;
using Lab1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab1.Data.Repositories
{
    public class TableRepository : ITableRepository
    {
        private readonly Lab1DbContext _context;

        public TableRepository(Lab1DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Table>> GetAllTablesAsync()
        {
            try
            {
                // Hämtar alla bord från databasen
                return await _context.Tables.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ett fel uppstod när borden hämtades.", ex);
            }
        }

        public async Task<Table> GetTableByIdAsync(int id)
        {
            try
            {
                // Hämtar ett specifikt bord baserat på ID
                return await _context.Tables.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ett fel uppstod när bordet med ID {id} hämtades.", ex);
            }
        }

        public async Task<Table> AddTableAsync(Table table)
        {
            try
            {
                // Lägger till ett nytt bord i databasen
                _context.Tables.Add(table);
                await _context.SaveChangesAsync();
                return table;
            }
            catch (Exception ex)
            {
                throw new Exception("Ett fel uppstod när bordet skulle sparas i databasen.", ex);
            }
        }

        public async Task<Table> UpdateTableAsync(Table table)
        {
            try
            {
                // Uppdaterar ett befintligt bord
                var existingTable = await _context.Tables.FindAsync(table.TableId);
                if (existingTable != null)
                {
                    existingTable.TableNumber = table.TableNumber;
                    existingTable.Seats = table.Seats;
                    _context.Entry(existingTable).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return existingTable;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ett fel uppstod när bordet med ID {table.TableId} skulle uppdateras.", ex);
            }
        }

        public async Task DeleteTableAsync(int id)
        {
            try
            {
                // Tar bort ett bord baserat på ID
                var table = await _context.Tables.FindAsync(id);
                if (table != null)
                {
                    _context.Tables.Remove(table);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ett fel uppstod när bordet med ID {id} skulle raderas.", ex);
            }
        }
    }
}
