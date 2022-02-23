using Microsoft.AspNetCore.Mvc;
using WebApplication11.Models;

namespace WebApplication11.Repository
{
    public interface IProductUserRepository
    {
        Task<List<ProductModel>> getcartItems(int id);
        Task<ProductModel> ProductDetails(int? id);
        Task<CartDataModel> AddCart(int id);
    }

}