using Ordering.Domain.Entities;

namespace Ordering.Application.Constracts.Persistance
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<List<Order>> GetByUserName(string userName);
    }
}
