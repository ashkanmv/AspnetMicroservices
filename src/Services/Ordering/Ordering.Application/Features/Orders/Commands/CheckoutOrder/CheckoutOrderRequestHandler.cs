using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Constracts.Infrastructure;
using Ordering.Application.Constracts.Persistance;
using Ordering.Application.Models;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderRequestHandler : IRequestHandler<CheckoutOrderRequest , long>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailService _emailService;
        private readonly Mapper _mapper;
        private readonly ILogger<Order> _logger;

        public CheckoutOrderRequestHandler(IOrderRepository orderRepository, IEmailService emailService, Mapper mapper, ILogger<Order> logger)
        {
            _orderRepository = orderRepository;
            _emailService = emailService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<long> Handle(CheckoutOrderRequest request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Order>(request);

            var newOrder = await _orderRepository.AddAsync(orderEntity);

            _logger.LogInformation($"Order With Id : {newOrder.Id} Created!");

            await SendEmail(request);

            return newOrder.Id;

        }

        private async Task SendEmail(CheckoutOrderRequest request)
        {
            var email = new Email() {Body = "", Subject = "", To = ""};

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception e)
            {
                _logger.LogError($"email not send : \n exception message : {e.Message} ");
            }
        }
    }
}
