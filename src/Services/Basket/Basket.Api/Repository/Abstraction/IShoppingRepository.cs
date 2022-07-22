using Basket.Api.Entities;

namespace Basket.Api.Repository.Abstraction
{
    public interface IShoppingRepository
    {
        Task<ShoppingCart?> GetShoppingCartAsync(string userName);
        Task<ShoppingCart> UpdateShoppingCartAsync(ShoppingCart cart);
        Task DeleteShoppingCartAsync(string userName);
    }
}
