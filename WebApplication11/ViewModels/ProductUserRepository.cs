using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication11.Data;
using WebApplication11.Models;

namespace WebApplication11.Repository
{
    public class ProductUserRepository : IProductUserRepository
    {
        private readonly DatabaseContaxt _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductUserRepository(DatabaseContaxt context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;

            webHostEnvironment = hostEnvironment;

        }
        public async Task<List<ProductModel>> getcartItems(int id)
        {
            List<ProductModel> products = new List<ProductModel>(); 
            products = await (from pro in _context.ProductModels
                            join cd in _context.Carts on pro.Id equals cd.ProductId
                            where pro.Id == id
                            select pro).ToListAsync();
            return products;
        }

        public async Task<ProductModel> ProductDetails(int? id)
        {
           
            var productModel = await _context.ProductModels
                .FirstOrDefaultAsync(m => m.Id == id);
            return productModel;
        }

        [HttpPost]
        public async Task<CartDataModel> AddCart(int id)
        {

            var obj = new CartDataModel()
            {
                ProductId = id,
                UserId = "123"
            };
            if (obj != null)
            {
                _context.Carts.Add(obj);
                _context.SaveChanges();
                return obj;
            }
            return null;
        }

    }
}
