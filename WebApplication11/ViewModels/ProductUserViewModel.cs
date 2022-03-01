using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
       // public async Task<List<string>> AddCart(int id)
       public async Task<CartDataModel> AddCart(int id)
        {
           // Hint(Guid id = Guid.NewGuid();)
          //Generate temp user id(is not the same as login id)

            var UserId = _httpContextAccessor.HttpContext.Session.GetString(SessionKeyName);
            // create an object of cart and add an item into it.


            // create a cookie and add the above objrct into cookie.
            // 

            //List<string> cart = new List<string> 
            
            //{
            //    id.ToString(),
            //    UserId 
            //};

            //if(cart != null)
            //{
            //    return cart ;

            //}
            var obj = new CartDataModel()
            {
                ProductId = id,
                UserId = UserId,
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
