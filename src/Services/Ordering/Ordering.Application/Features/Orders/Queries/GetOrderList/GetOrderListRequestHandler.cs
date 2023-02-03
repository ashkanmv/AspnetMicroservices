using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Constracts.Persistance;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Queries.GetOrderList
{
    public class GetOrderListRequestHandler : IRequestHandler<GetOrderListRequest,List<OrdersVs>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly Mapper _mapper;
        private readonly ILogger<Order> _logger;

        public GetOrderListRequestHandler(IOrderRepository orderRepository, Mapper mapper, ILogger<Order> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<OrdersVs>> Handle(GetOrderListRequest request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetByUserName(request.UserName);
            if (orders == null)
            {
                _logger.LogError($"Order for userName : {request.UserName} Not Found!");
            }

            return _mapper.Map<List<OrdersVs>>(orders);
        }
    }
}
