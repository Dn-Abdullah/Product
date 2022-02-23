﻿
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

namespace WebApplication11.Controllers
{

    public class ProductUserController : Controller

    {
        private readonly DatabaseContaxt _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public IProductUserRepository _ProductUserRepository;
        public ProductUserController(DatabaseContaxt context, IWebHostEnvironment hostEnvironment, IProductUserRepository productUserRepository)
        {
            _context = context;

            webHostEnvironment = hostEnvironment;
            _ProductUserRepository = productUserRepository;

        }

        // GET: ProductModels
        public async Task<IActionResult> Index()
        {
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
