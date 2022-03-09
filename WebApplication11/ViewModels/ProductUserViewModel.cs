using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ubiety.Dns.Core;
using WebApplication11.Data;
using WebApplication11.Models;

namespace WebApplication11.Repository
{
    public class ProductUserViewModel : IProductUserViewModel
    {
        public const string SessionKeyName = "_Id";
        IHttpContextAccessor _httpContextAccessor;
        private readonly DatabaseContaxt _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductUserViewModel(DatabaseContaxt context, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;

            webHostEnvironment = hostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<ProductModel>> getcartItems(int id)
        {
            string UId = _httpContextAccessor.HttpContext.Request.Cookies["key"];
            List<ProductModel> products = new List<ProductModel>();
           
            products = await (from pro in _context.ProductModels
                              join cd in _context.Carts on pro.Id equals cd.ProductId
                              where cd.UserId == UId
                              select pro).ToListAsync();
            
            return products;
        }

        public async Task<ProductModel> ProductDetails(int? id)
        {

            var productModel = await _context.ProductModels
                .FirstOrDefaultAsync(m => m.Id == id);
            return productModel;
        }
        public async Task<CartDataModel> Del(string id)
        {
            if (id == null)
            {
                return null;
            }

            var Cart = await _context.Carts
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (Cart == null)
            {
                return null;
            }
            return Cart;

        }

        public async Task<CartDataModel> DelConfirmed(string id)
        {
            
            var delproduct = await _context.Carts.Where(x=>x.UserId==id).FirstOrDefaultAsync();
            _context.Carts.Remove(delproduct);
            await _context.SaveChangesAsync();
            return delproduct;
        }

        [HttpPost]
       // public async Task<List<string>> AddCart(int id)
       public async Task<CartDataModel> AddCart(int id)
        {
          
            string UId = _httpContextAccessor.HttpContext.Request.Cookies["key"];
        
            var obj = new CartDataModel()
            {
                ProductId = id,
                UserId = UId,
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
