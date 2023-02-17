using System.Net;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Orders.Queries.GetOrderList;

namespace Ordering.API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("[action]/{userName}")]
        [ProducesResponseType(typeof(IEnumerable<OrdersVs>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrdersVs>>> GetOrderList(string userName)
        {
            var request = new GetOrderListRequest(userName);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

    }
}
