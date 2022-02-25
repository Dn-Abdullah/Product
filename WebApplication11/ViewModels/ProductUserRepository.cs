using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication11.Data;
using WebApplication11.Models;

namespace WebApplication11.Repository
{
    public class ProductUserRepository : IProductUserRepository
    {
        public const string SessionKeyName = "_Name";
        IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly DatabaseContaxt _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductUserRepository(DatabaseContaxt context, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
        {
            _context = context;

            webHostEnvironment = hostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
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
            if (string.IsNullOrEmpty(_httpContextAccessor.HttpContext.Session.GetString(SessionKeyName)))
            {
                _httpContextAccessor.HttpContext.Session.SetString(SessionKeyName, _userManager.GetUserId(_httpContextAccessor.HttpContext.User));
            }
            var UserId = _httpContextAccessor.HttpContext.Session.GetString(SessionKeyName);

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
