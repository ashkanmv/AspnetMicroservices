using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Constracts.Persistance;
using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Unit>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateOrderCommand> _logger;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<UpdateOrderCommand> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = await _orderRepository.GetByIdAsync(request.Id);

            if (orderEntity == null)
            {
                throw new NotFoundException(nameof(Order),request.Id);
            }

            var order = _mapper.Map(request, orderEntity, typeof(UpdateOrderCommand), typeof(Order));

            _logger.LogInformation("Order updated with id {Id}",request.Id);

            return Unit.Value;
        }
    }
}
