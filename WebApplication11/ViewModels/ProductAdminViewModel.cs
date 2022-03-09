using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication11.Data;
using WebApplication11.Models;
//using Microsoft.AspNetCore.Identity;
//using System.Security.Claims;

namespace WebApplication11.ViewModels
{
    public class ProductAdminViewModel : IProductAdminViewModel
    {
        //IHttpContextAccessor _httpContextAccessor;
        //private readonly UserManager<IdentityUser> _userManager;
        private readonly DatabaseContaxt _contaxt;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductAdminViewModel(DatabaseContaxt contaxt, IWebHostEnvironment hostEnvironment)
        {
            _contaxt = contaxt;
            _webHostEnvironment = hostEnvironment;
            //_userManager = userManager;
            //_httpContextAccessor = httpContextAccessor;
        }

        public async Task<ProductModel> ProCreatee(IFormFile files, ProductModel pm)
        {
          
                    //Getting FileName
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads");

                    var fileName = Path.GetFileName(files.FileName);
                    //Getting file Extension
                    var fileExtension = Path.GetExtension(fileName);
                    // concatenating  FileName + FileExtension
                    var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);
                    string filePath = Path.Combine(uploadsFolder, newFileName);


            //var abc = _userManager.GetUserId(HttpContext.User);

            var obj = new ProductModel()
            
                    {
                        //  Id = 0,

                        ProductName = pm.ProductName,
                        ProductPrice = pm.ProductPrice,
                        Description = pm.Description,
                        ShortDescription = pm.ShortDescription,
                        ProfilePicture = newFileName,


                    };

                    using (var fileStream = new FileStream(filePath, FileMode.Create)) //temporry arry binary
                    {
                        files.CopyTo(fileStream);
                    }

                    _contaxt.ProductModels.Add(obj);
                    _contaxt.SaveChanges();
                   
                
            return obj;
        }

       
        public async Task<ProductModel> Del(int? id)
        {
            if (id == 0)
            {
                return null;
            }

            var productModel = await _contaxt.ProductModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productModel == null)
            {
                return null;
            }
            return productModel;

        }

        public async Task<ProductModel> DelConfirmed(int id)
        {
            var delproduct = await _contaxt.ProductModels.FindAsync(id);
            _contaxt.ProductModels.Remove(delproduct);
            await _contaxt.SaveChangesAsync();
            return delproduct;
        }

        public async Task<ProductModel> ProductDetails(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var productDetail = await _contaxt.ProductModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productDetail == null)
            {
                return null;
            }

            return productDetail;
        }

        public async Task<ProductModel> ProductUpdate(int? id)
        {
            var Editproduct = await _contaxt.ProductModels.FindAsync(id);
            return Editproduct;
        }


        public async Task<int> Update(int id, [Bind("Id,ProductName,ProductPrice,ProfilePicture,Description,ShortDescription")] ProductModel productModel)
        {
            _contaxt.Update(productModel);
            await _contaxt.SaveChangesAsync();
            return 1;
        }



    }
}
