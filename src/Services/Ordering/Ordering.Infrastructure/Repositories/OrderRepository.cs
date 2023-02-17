using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Constracts.Persistance;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistance;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order> , IOrderRepository
    {
        public OrderRepository(OrderContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Order>> GetByUserName(string userName)
        {
            return await _dbContext.Orders.Where(o => o.UserName == userName).ToListAsync();
        }
    }
}
