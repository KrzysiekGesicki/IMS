using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Plugins.EFCore
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMSContext _db;

        public ProductRepository(IMSContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Product>> GetProductsByNameAsync (string name)
        {
            return await _db.Products.Where(p => p.ProductName.Contains(name, StringComparison.OrdinalIgnoreCase) || string.IsNullOrEmpty(name)).ToListAsync();
        }
    }
}
