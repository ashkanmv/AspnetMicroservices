using MediatR;

namespace Ordering.Application.Features.Orders.Queries.GetOrderList
{
    public class GetOrderListRequest : IRequest<List<OrdersVs>>
    {
        public GetOrderListRequest(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; set; }
    }
}
