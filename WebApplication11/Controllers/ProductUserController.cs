
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
        public string id;
        // string id;
        //  private readonly UserManager<IdentityUser> _userManager;
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



        //public Guid SessionGUID()
        //{
        //    //if (HttpContext.Current.Request.Cookies["SessionGUID"])
        //    //{
        //    //    //return the SessionGUID
        //    //    return HttpContext.Current.Request.Cookies["SessionGUID"].value as Guid;
        //    //}
        //    //else//new visit
        //    //{


        //        //set cookie to a new random Guid
        //        var _guid = Guid.NewGuid();
        //        CookieOptions option = new CookieOptions
        //        {

        //            Expires = DateTime.Now.AddMinutes(5)
        //        };
        //        Response.Cookies.Append("Key", _guid.ToString() , option);
        //      //  Response.Cookies.Add(guidCookie);
        //        return _guid;
        //   // }
        //}

        // GET: ProductModels
        public async Task<IActionResult> Index()
        {
            string cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies["key"];
            
            if (cookieValueFromContext == null)
            {
                Guid abc = Guid.NewGuid();
                var value = Set("key", abc.ToString(), 03);
 
            }
            //Delete the cookie object  
           // Remove("key");
            return View(await _context.ProductModels.ToListAsync());


        }
        public string Get(string key)
        {
            return Request.Cookies[key];
        }
        public string Set(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();
            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);
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
        public async Task<IActionResult> Delete(int? id)
        {
            var productdelete = await _ProductUserRepository.Del(id);
            return View(productdelete);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productModel = await _ProductUserRepository.DelConfirmed(id);
            return RedirectToAction(nameof(Index));
        }

        // getCart
        public async Task<IActionResult> GetCart(int id)
        {
            var products = await _ProductUserRepository.getcartItems(id);
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
            return RedirectToAction("Index");
        }

    }
}
