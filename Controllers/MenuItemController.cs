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
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemRepository _menuItemRepository;

        public MenuItemController(IMenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
        }

        // GET: api/MenuItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItemDTO>>> GetAllMenuItems()
        {
            var menuItems = await _menuItemRepository.GetAllMenuItemsAsync();

            if (menuItems == null || !menuItems.Any())
            {
                return NotFound("Inga menyalternativ hittades."); // Returnerar 404 om inga menyalternativ finns
            }

            var menuItemDTOs = menuItems.Select(m => new MenuItemDTO
            {
                MenuItemId = m.MenuItemId,
                Name = m.Name,
                Price = m.Price,
                IsAvailable = m.IsAvailable
            });

            return Ok(menuItemDTOs);
        }

        // GET: api/MenuItem/{menuItemId}
        [HttpGet("{menuItemId}")]
        public async Task<ActionResult<MenuItemDTO>> GetMenuItemById(int menuItemId)
        {
            var menuItem = await _menuItemRepository.GetMenuItemByIdAsync(menuItemId);
            if (menuItem == null)
            {
                return NotFound($"Menyalternativet med ID {menuItemId} hittades inte."); // Returnerar 404 om menyalternativet inte finns
            }

            var menuItemDTO = new MenuItemDTO
            {
                MenuItemId = menuItem.MenuItemId,
                Name = menuItem.Name,
                Price = menuItem.Price,
                IsAvailable = menuItem.IsAvailable
            };

            return Ok(menuItemDTO);
        }

        // POST: api/MenuItem
        [HttpPost]
        public async Task<ActionResult<MenuItemDTO>> AddMenuItem(CreateMenuItemDTO createMenuItemDTO)
        {
            if (string.IsNullOrEmpty(createMenuItemDTO.Name) || createMenuItemDTO.Price <= 0)
            {
                return BadRequest("Namn och pris måste anges och priset måste vara större än noll."); // Validering av indata
            }

            var menuItem = new MenuItem
            {
                Name = createMenuItemDTO.Name,
                Price = createMenuItemDTO.Price,
                IsAvailable = createMenuItemDTO.IsAvailable
            };

            var addedMenuItem = await _menuItemRepository.AddMenuItemAsync(menuItem);

            var addedMenuItemDTO = new MenuItemDTO
            {
                MenuItemId = addedMenuItem.MenuItemId,
                Name = addedMenuItem.Name,
                Price = addedMenuItem.Price,
                IsAvailable = addedMenuItem.IsAvailable
            };

            return CreatedAtAction(nameof(GetMenuItemById), new { menuItemId = addedMenuItem.MenuItemId }, addedMenuItemDTO);
        }

        // PUT: api/MenuItem/{menuItemId}
        [HttpPut("{menuItemId}")]
        public async Task<IActionResult> UpdateMenuItem(int menuItemId, UpdateMenuItemDTO updateMenuItemDTO)
        {
            if (menuItemId != updateMenuItemDTO.MenuItemId)
            {
                return BadRequest("Menyalternativ-ID matchar inte."); // Kontrollera att ID:na matchar
            }

            var existingMenuItem = await _menuItemRepository.GetMenuItemByIdAsync(menuItemId);
            if (existingMenuItem == null)
            {
                return NotFound($"Menyalternativet med ID {menuItemId} hittades inte."); // Returnerar 404 om menyalternativet inte finns
            }

            existingMenuItem.Name = updateMenuItemDTO.Name;
            existingMenuItem.Price = updateMenuItemDTO.Price;
            existingMenuItem.IsAvailable = updateMenuItemDTO.IsAvailable;

            var updatedMenuItem = await _menuItemRepository.UpdateMenuItemAsync(existingMenuItem);
            if (updatedMenuItem == null)
            {
                return NotFound(); // Returnerar 404 om uppdateringen misslyckas
            }

            return NoContent(); // Returnerar 204 vid lyckad uppdatering
        }

        // DELETE: api/MenuItem/{menuItemId}
        [HttpDelete("{menuItemId}")]
        public async Task<IActionResult> DeleteMenuItem(int menuItemId)
        {
            var existingMenuItem = await _menuItemRepository.GetMenuItemByIdAsync(menuItemId);
            if (existingMenuItem == null)
            {
                return NotFound($"Menyalternativet med ID {menuItemId} hittades inte."); // Returnerar 404 om menyalternativet inte finns
            }

            await _menuItemRepository.DeleteMenuItemAsync(menuItemId);
            return NoContent(); // Returnerar 204 efter borttagning
        }
    }
}
