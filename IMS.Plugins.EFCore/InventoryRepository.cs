using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;

namespace IMS.Plugins.EFCore
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IMSContext _db;

        public InventoryRepository(IMSContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Inventory>> GetInventoriesByName(string name)
        {
            return await _db.Inventories.Where(i => i.InventoryName.Contains(name, StringComparison.OrdinalIgnoreCase) || string.IsNullOrEmpty(name)).ToListAsync();                         
        }

        public async Task AddInventoryAsync(Inventory inventory)
        {
            _db.Inventories.Add(inventory);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateInventory(Inventory inventory)
        {
            var inv = await _db.Inventories.FindAsync(inventory.InventoryId);
            if (inv != null)
            {
                inv.InventoryName = inventory.InventoryName;
                inv.Price = inventory.Price;
                inv.Quantity = inventory.Quantity;
                await _db.SaveChangesAsync();
            }
        }
    }
}