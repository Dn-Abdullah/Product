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
using WebApplication11.ViewModels;

namespace WebApplication11.Controllers
{
    [Authorize]

    public class ProductAdminController : Controller
    {
        private readonly DatabaseContaxt _contaxt;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IProductAdminRepository _ProductAdminRepository;
        //  public IProductUserRepository ProductRepository;

        public ProductAdminController(IProductAdminRepository productAdminRepository, IWebHostEnvironment webHostEnvironment, DatabaseContaxt contaxt)
        {
           _contaxt = contaxt;
            _webHostEnvironment = webHostEnvironment;
            _ProductAdminRepository = productAdminRepository;

        }

        public async Task<IActionResult> Index()
        {
            return View(await _contaxt.ProductModels.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()

        {
            return View();
        }
       

        // GET: ProductModels/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            var productModel = await _ProductAdminRepository.ProductDetails(id);
            return View(productModel);
        }

        // GET: ProductModels/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            var productdelete = await _ProductAdminRepository.Del(id);
            return View(productdelete);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productModel = await _ProductAdminRepository.DelConfirmed(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var EditProduct = await _ProductAdminRepository.ProductUpdate(id);
           
            if (EditProduct == null)
            {
                return NotFound();
            }
            return View(EditProduct);
        }

        // POST: ProductModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductName,ProductPrice,ProfilePicture,Description")] ProductModel productModel)
        {
            if (id != productModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _ProductAdminRepository.Update(id, productModel);
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
                //      return RedirectToAction(nameof(Index));
                return RedirectToAction("Index");
            }
            return View(productModel);
        }

        private bool ProductModelExists(int id)
        {
            return _contaxt.ProductModels.Any(e => e.Id == id);
        }

        public IActionResult Createe()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Create([Bind("Id,ProductName,ProductPrice,ProfilePicture")] ProductModel productModel)

        public async Task<IActionResult> Create(IFormFile files, ProductModel pm)
        { var add = _ProductAdminRepository.ProCreatee(files, pm);
            if (add == null)
            {   
                    return NotFound();
            }
            return View();
            ModelState.Clear();
          
        }



        //public async Task<IActionResult> Createe(IFormFile files, ProductModel pm)
        //{
        //    if (files != null)
        //    {
        //        if (files.Length > 0)
        //        {
        //            //Getting FileName
        //            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads");

        //            var fileName = Path.GetFileName(files.FileName);
        //            //Getting file Extension
        //            var fileExtension = Path.GetExtension(fileName);
        //            // concatenating  FileName + FileExtension
        //            var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);
        //            string filePath = Path.Combine(uploadsFolder, newFileName);

        //            // ProductModel objm = new ProductModel();
        //            //  var objfilesa = new ProductModel()

        //            var obj = new ProductModel()

        //            {
        //                //  Id = 0,

        //                ProductName = pm.ProductName,
        //                ProductPrice = pm.ProductPrice,
        //                Description = pm.Description,
        //                ProfilePicture = newFileName,


        //            };

        //            using (var fileStream = new FileStream(filePath, FileMode.Create)) //temporry arry binary
        //            {
        //                files.CopyTo(fileStream);
        //            }

        //            _contaxt.ProductModels.Add(obj);
        //            _contaxt.SaveChanges();
        //            ModelState.Clear();
        //        }
        //    }
        //    return View();
        //}


    }
}

//[HttpPost]
//[ValidateAntiForgeryToken]
//// public async Task<IActionResult> Create([Bind("Id,ProductName,ProductPrice,ProfilePicture")] ProductModel productModel)

//public async Task<IActionResult> Create(IFormFile files, ProductModel pm)
//{
//    if (files != null)
//    {
//        if (files.Length > 0)
//        {
//            //Getting FileName
//            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Uploads");

//            var fileName = Path.GetFileName(files.FileName);
//            //Getting file Extension
//            var fileExtension = Path.GetExtension(fileName);
//            // concatenating  FileName + FileExtension
//            var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);
//            string filePath = Path.Combine(uploadsFolder, newFileName);

//            // ProductModel objm = new ProductModel();
//            //  var objfilesa = new ProductModel()

//            var obj = new ProductModel()

//            {
//                //  Id = 0,

//                ProductName = pm.ProductName,
//                ProductPrice = pm.ProductPrice,
//                Description = pm.Description,
//                ProfilePicture = newFileName,


//            };

//            using (var fileStream = new FileStream(filePath, FileMode.Create)) //temporry arry binary
//            {
//                files.CopyTo(fileStream);
//            }

//            _context.ProductModels.Add(obj);
//            _context.SaveChanges();
//            ModelState.Clear();
//        }
//    }
//    return View();
//}







// GET: ProductModels/Edit/5
//public async Task<IActionResult> Edit(int? id)
//{
//    if (id == null)
//    {
//        return NotFound();
//    }

//    var productModel = await _context.ProductModels.FindAsync(id);
//    if (productModel == null)
//    {
//        return NotFound();
//    }
//    return View(productModel);
//}

//// POST: ProductModels/Edit/5
//// To protect from overposting attacks, enable the specific properties you want to bind to.
//// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//[HttpPost]
//public async Task<IActionResult> Edit(int id, [Bind("Id,ProductName,ProductPrice,ProfilePicture,Description")] ProductModel productModel)
//{
//    if (id != productModel.Id)
//    {
//        return NotFound();
//    }

//    if (ModelState.IsValid)
//    {
//        try
//        {
//            _context.Update(productModel);
//            await _context.SaveChangesAsync();
//        }
//        catch (DbUpdateConcurrencyException)
//        {
//            if (!ProductModelExists(productModel.Id))
//            {
//                return NotFound();
//            }
//            else
//            {
//                throw;
//            }
//        }
//        //      return RedirectToAction(nameof(Index));
//        return RedirectToAction("Index");
//    }
//    return View(productModel);
//}











//public async Task<IActionResult> Delete(int? id)
//{
//    if (id == null)
//    {
//        return NotFound();
//    }

//    var productModel = await _context.ProductModels
//        .FirstOrDefaultAsync(m => m.Id == id);
//    if (productModel == null)
//    {
//        return NotFound();
//    }

//    return View(productModel);
//}

//// POST: ProductModels/Delete/5
//[HttpPost, ActionName("Delete")]
//[ValidateAntiForgeryToken]
//public async Task<IActionResult> DeleteConfirmed(int id)
//{
//    var productModel = await _context.ProductModels.FindAsync(id);
//    _context.ProductModels.Remove(productModel);
//    await _context.SaveChangesAsync();
//    return RedirectToAction(nameof(Index));
//}




//[HttpPost]
//public IActionResult DetailsSending(int id)
//{

//    var obj = new CartDataModel()
//    {
//        ProductId = id,
//        UserId = "123"
//    };
//    if (obj != null)
//    {
//        _context.Carts.Add(obj);
//        _context.SaveChanges();
//        return RedirectToAction("Index");
//    }
//    return View();
//}
//public async Task<IActionResult> GetCart(int id)
//{
//    var products = await _ProductAdminRepository.getcartItems(id);
//    return View(products);

//}


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

