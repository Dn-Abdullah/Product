using Microsoft.AspNetCore.Mvc;
using WebApplication11.Models;

namespace WebApplication11.ViewModels
{
    public interface IProductAdminViewModel
    {
        
     
        Task<ProductModel> Del(int? id);
        Task<ProductModel> DelConfirmed(int id);
        Task<ProductModel> ProductDetails(int? id);
        Task<ProductModel> ProductUpdate(int? id);
        Task<int> Update(int id, [Bind("Id,ProductName,ProductPrice,ProfilePicture,Description")] ProductModel productModel);
        Task<ProductModel> ProCreatee(IFormFile files, ProductModel pm);


    }
}
