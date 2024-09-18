using Lab1.Data.Repositories.IRepositories;
using Lab1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab1.Data.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly Lab1DbContext _context;

        public MenuItemRepository(Lab1DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MenuItem>> GetAllMenuItemsAsync()
        {
            try
            {
                // Hämtar alla menyposter från databasen
                return await _context.MenuItems.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ett fel uppstod när menyposterna hämtades.", ex);
            }
        }

        public async Task<MenuItem> GetMenuItemByIdAsync(int id)
        {
            try
            {
                // Hämtar en specifik menypost baserat på ID
                return await _context.MenuItems.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ett fel uppstod när menyposten med ID {id} hämtades.", ex);
            }
        }

        public async Task<MenuItem> AddMenuItemAsync(MenuItem menuItem)
        {
            try
            {
                // Lägger till en ny menypost i databasen
                _context.MenuItems.Add(menuItem);
                await _context.SaveChangesAsync();
                return menuItem;
            }
            catch (Exception ex)
            {
                throw new Exception("Ett fel uppstod när menyposten skulle sparas i databasen.", ex);
            }
        }

        public async Task<MenuItem> UpdateMenuItemAsync(MenuItem menuItem)
        {
            try
            {
                // Uppdaterar en befintlig menypost
                var existingMenuItem = await _context.MenuItems.FindAsync(menuItem.MenuItemId);
                if (existingMenuItem != null)
                {
                    existingMenuItem.Name = menuItem.Name;
                    existingMenuItem.Price = menuItem.Price;
                    existingMenuItem.IsAvailable = menuItem.IsAvailable;
                    _context.Entry(existingMenuItem).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return existingMenuItem;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ett fel uppstod när menyposten med ID {menuItem.MenuItemId} skulle uppdateras.", ex);
            }
        }

        public async Task DeleteMenuItemAsync(int id)
        {
            try
            {
                // Tar bort en menypost baserat på ID
                var menuItem = await _context.MenuItems.FindAsync(id);
                if (menuItem != null)
                {
                    _context.MenuItems.Remove(menuItem);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ett fel uppstod när menyposten med ID {id} skulle raderas.", ex);
            }
        }
    }
}
