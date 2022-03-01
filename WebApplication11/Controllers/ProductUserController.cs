
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
        string id;
      //  private readonly UserManager<IdentityUser> _userManager;
        private readonly DatabaseContaxt _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public IProductUserViewModel _ProductUserRepository;
        public ProductUserController(DatabaseContaxt context, IWebHostEnvironment hostEnvironment, IProductUserViewModel productUserRepository)
        {
            _context = context;

            webHostEnvironment = hostEnvironment;
            _ProductUserRepository = productUserRepository;
            
        }



        public Guid SessionGUID()
        {
            //if (HttpContext.Current.Request.Cookies["SessionGUID"])
            //{
            //    //return the SessionGUID
            //    return HttpContext.Current.Request.Cookies["SessionGUID"].value as Guid;
            //}
            //else//new visit
            //{



                //set cookie to a new random Guid
                var _guid = Guid.NewGuid();
                CookieOptions option = new CookieOptions
                {

                    Expires = DateTime.Now.AddMinutes(5)
                };
                Response.Cookies.Append("User", _guid.ToString() , option);
              //  Response.Cookies.Add(guidCookie);
                return _guid;
           // }
        }

        // GET: ProductModels
        public async Task<IActionResult> Index()
        {
            //var abc = _userManager.GetUserId(HttpContext.User);
            //  var id="";

            if (id == null) { 
               var id = SessionGUID();
         
            

            CookieOptions option = new CookieOptions
            {
                
                Expires = DateTime.Now.AddMinutes(5)
            };
         Response.Cookies.Append("User",id.ToString(), option);
            }
            return View(await _context.ProductModels.ToListAsync());
          
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
