namespace API.Orders
{
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Commands;
    using Application.Queries;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Utils;

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IMediator _bus;

        public OrderController(IMediator bus)
        {
            _bus = bus;
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetCustomerOrders(string customerId)
        {
            return FromResult(await _bus.Send(new GetCustomerOrdersQuery(customerId)));
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrder(string orderId)
        {
            return FromResult(await _bus.Send(new GetOrderQuery(orderId)));
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            return FromResult(await _bus.Send(new GetAllOrdersQuery()));
        }

        [HttpPost("/buyer")]
        public async Task<IActionResult> CreateOrderBuyer([FromBody] CreateOrderDto dto)
        {
            var command = new CreateOrderBuyerCommand(
                dto.DeliveryDate,
                dto.CustomerId,
                dto.CustomerType,
                dto.CustomerName,
                dto.CustomerEmail,
                dto.CustomerTaxNumber,
                dto.Products.Select(p => new CreateOrderBuyerCommand.Product(p.Id, p.Name, p.Description, p.Ingredients, p.Price)));

            return FromResult(await _bus.Send(command));
        }

        [HttpPost("/charity")]
        public async Task<IActionResult> CreateOrderCharity([FromBody] CreateOrderDto dto)
        {
            var command = new CreateOrderCharityCommand(
                dto.DeliveryDate,
                dto.CustomerId,
                dto.CustomerType,
                dto.CustomerName,
                dto.CustomerEmail,
                dto.CustomerTaxNumber,
                dto.Products.Select(p => new CreateOrderCharityCommand.Product(p.Id, p.Name, p.Description, p.Ingredients, p.Price)));

            return FromResult(await _bus.Send(command));
        }

        [HttpPost("/buyer/update")]
        public async Task<IActionResult> UpdateScheduledDate([FromBody] UpdateScheduledDateDto dto)
        {
            return FromResult(await _bus.Send(new UpdateScheduledDateCommand(dto.CustomerId, dto.OrderNumber, dto.NewScheduleDate)));
        }

        [HttpPost("/buyer/cancel")]
        public async Task<IActionResult> CancelOrder([FromBody] CancelOrderDto dto)
        {
            return FromResult(await _bus.Send(new CancelOrderBuyerCommand(dto.CustomerId, dto.OrderNumber)));
        }

        [HttpPost("/delivery")]
        public async Task<IActionResult> DeliveryOrder([FromBody] DeliveryOrderDto dto)
        {
            return FromResult(await _bus.Send(new DeliveryOrderCommand(dto.CustomerId, dto.OrderNumber)));
        }
    }
}