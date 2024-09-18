using Lab1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab1.Data.Repositories.IRepositories
{
    public interface ITableRepository
    {
        Task<IEnumerable<Table>> GetAllTablesAsync();
        Task<Table> GetTableByIdAsync(int id);
        Task<Table> AddTableAsync(Table table);
        Task<Table> UpdateTableAsync(Table table);
        Task DeleteTableAsync(int id);
    }
}
