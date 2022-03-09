using Microsoft.AspNetCore.Mvc;
using WebApplication11.Models;

namespace WebApplication11.Repository
{
    public interface IProductUserViewModel
    {
        Task<List<ProductModel>> getcartItems(int id);
        //Task<List<string>> AddCart(int id);
        Task<ProductModel> ProductDetails(int? id);
        Task<CartDataModel> AddCart(int id);
        Task<CartDataModel> Del(int id);
        Task<List<CartDataModel>> DeleteAll();
    }

}