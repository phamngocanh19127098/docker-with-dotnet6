using DataAccess.DTO.Request;
using DataAccess.DTO.Request.Class;
using DataAccess.DTO.Request.Order;
using DataAccess.DTO.Response;
using DataAccess.Exceptions;
using DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace TicketOrderApi.Controllers
{
  [Route(Helpers.SettingVersionApi.ApiVersion)]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderService;

        public OrderController(IOrderServices orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Get list bus
        /// </summary>
        [HttpGet(Name = "Get")]
        public async Task<ActionResult<BaseResponsePagingViewModel<OrderResponse>>> GetAllClass
            ([FromQuery] OrderResponse request, [FromQuery] PagingRequest paging)
        {
            try
            {
                return await _orderService.GetAllOrder(paging);
            }
            catch (ErrorResponse ex)
            {
                return BadRequest(ex.Error);
            }
        }

        /// <summary>
        /// Create Bus
        /// </summary>
        [HttpPost()]
        public async Task<ActionResult<BaseResponseViewModel<OrderResponse>>> CreateOrder
            ([FromBody] CreateOrderRequest request)
        {
            try
            {
                return await _orderService.CreateOrder(request);
            }
            catch (ErrorResponse ex)
            {
                return BadRequest(ex.Error);
            }
        }

        /// <summary>
        /// Update Bus
        /// </summary>
        [HttpPut("{orderId}")]
        public async Task<ActionResult<BaseResponseViewModel<OrderResponse>>> UpdateOrder
            ([FromRoute] int orderId, [FromBody] UpdateOrderRequest request)
        {
            try
            {
                return await _orderService.UpdateOrder(orderId, request);
            }
            catch (ErrorResponse ex)
            {
                return BadRequest(ex.Error);
            }
        }
    }
}