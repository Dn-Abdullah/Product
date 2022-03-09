
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication11.Data;
using WebApplication11.Models;
using WebApplication11.Repository;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace WebApplication11.Controllers
{

    public class ProductUserController : Controller

    {

        IHttpContextAccessor _httpContextAccessor;
        private readonly DatabaseContaxt _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public IProductUserViewModel _ProductUserRepository;
        public ProductUserController(DatabaseContaxt context, IWebHostEnvironment hostEnvironment, IProductUserViewModel productUserRepository, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;

            webHostEnvironment = hostEnvironment;
            _ProductUserRepository = productUserRepository;
            _httpContextAccessor = httpContextAccessor;
        }





        // GET: ProductModels
        public async Task<IActionResult> Index()
        {
            string cookie = _httpContextAccessor.HttpContext.Request.Cookies["key"];
            CookieOptions option = new CookieOptions();
            if (cookie == null)
            {
                Guid abc = Guid.NewGuid();
                var value = Set("key", abc.ToString(), 2);
            }
            //else
            //{
            //    if (!isset($_COOKIE['key']))
            //    {
            //        DeleteId(cookie);
            //        exit;
            //    }
            //}
            return View(await _context.ProductModels.ToListAsync());
        }
        public string Set(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();

            option.Expires = DateTime.Now.AddDays(expireTime.Value);

            Response.Cookies.Append(key, value, option);
            return value;
        }
        public void Remove(string key)
        {
            Response.Cookies.Delete(key);
        }
        // Detail
        public async Task<IActionResult> Details(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var productdetails = await _ProductUserRepository.ProductDetails(id);
            if (productdetails == null)
            {
                return NotFound();
            }
            return View(productdetails);
        }
        // Delete product from cart
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {

            var productdelete = await _ProductUserRepository.Del(id);
            //   return View(productdelete);
            return RedirectToAction(nameof(GetCart));
        }
       
        public async Task<IActionResult> EmptyCart(string id)
        {
            var productModel = await _ProductUserRepository.DeleteAll();
            return RedirectToAction(nameof(GetCart));
        }

        // getCart
        public async Task<IActionResult> GetCart(int id)
        {
            string cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies["key"];
            var products = await _ProductUserRepository.getcartItems(id);
             ViewBag.list = products.Count;
           // ViewBag.list = 0;
            ViewBag.messaage = cookieValueFromContext;
            return View(products);

        }
        // add Cart
        public async Task<IActionResult> DetailsSending(int id)
        {
            var Add = await _ProductUserRepository.AddCart(id);
            if (id == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(GetCart));
        }
        public async Task DeleteId(string id)
        {
            var clearCart = await _context.Carts.Where(x => x.UserId == id).FirstOrDefaultAsync();
            _context.Carts.Remove(clearCart);
            await _context.SaveChangesAsync();
        }

    }
}
