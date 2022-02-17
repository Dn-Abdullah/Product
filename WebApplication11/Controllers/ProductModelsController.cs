﻿#nullable disable
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

namespace WebApplication11.Controllers
{
    [Authorize]
    public class ProductModelsController : Controller

    {
        // int Count = 0;

        private readonly DatabaseContaxt _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductModelsController(DatabaseContaxt context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;

            webHostEnvironment = hostEnvironment;

        }

        // GET: ProductModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductModels.ToListAsync());
        }

        // GET: ProductModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _context.ProductModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productModel == null)
            {
                return NotFound();
            }

            return View(productModel);
        }



        // GET: ProductModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _context.ProductModels.FindAsync(id);
            if (productModel == null)
            {
                return NotFound();
            }
            return View(productModel);
        }

        // POST: ProductModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductName,ProductPrice,ProfilePicture")] ProductModel productModel)
        {
            if (id != productModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductModelExists(productModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productModel);
        }

        // GET: ProductModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _context.ProductModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productModel == null)
            {
                return NotFound();
            }

            return View(productModel);
        }

        // POST: ProductModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productModel = await _context.ProductModels.FindAsync(id);
            _context.ProductModels.Remove(productModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductModelExists(int id)
        {
            return _context.ProductModels.Any(e => e.Id == id);
        }


        [HttpPost]
        public IActionResult DetailsSending(int id)
        {

            var obj = new CartData()
            {
                ProductId = id,
                UserId = "123"
            };
            if (obj != null)
            {
                _context.Carts.Add(obj);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }


        public async Task<IActionResult> getcartItems(int id)
        {
            var person = new List<ProductModel>();

            person = await (from pro in _context.ProductModels
                            join cd in _context.Carts on pro.Id equals cd.ProductId
                            where pro.Id == id
                            select pro).ToListAsync();
            return View(person);
        }

        //private string ProcessUploadedFile(ProductModel model)
        //{
        //    string uniqueFileName = null;

        //    if (model.ProfilePicture != null)
        //    {
        //        string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Uploads");
        //        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfilePicture.FileName;
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            model.ProfilePicture.CopyTo(fileStream);
        //        }
        //    }

        //    return uniqueFileName;
        //}


    }
}
