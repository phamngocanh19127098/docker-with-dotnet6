using DataAccess.DTO.Request;
using DataAccess.DTO.Request.Ticket;
using DataAccess.DTO.Response;
using DataAccess.Exceptions;
using DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace TicketOrderApi.Controllers
{
    [Route(Helpers.SettingVersionApi.ApiVersion)]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketServices _ticketService;

        public TicketController(ITicketServices ticketService)
        {
            _ticketService = ticketService;
        }

        /// <summary>
        /// Get list bus
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<BaseResponsePagingViewModel<TicketInformationResponse>>> GetAllTicket
            ([FromQuery] TicketResponse request, [FromQuery] PagingRequest paging)
        {
            try
            {
                return await _ticketService.GetAllTicket(paging);
            }
            catch (ErrorResponse ex)
            {
                return BadRequest(ex.Error);
            }
        }

        /// <summary>
        /// Create Bus
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<BaseResponseViewModel<TicketInformationResponse>>> CreateTicket
            ([FromBody] CreateTicketRequest request)
        {
            try
            {
                return await _ticketService.CreateTicket(request);
            }
            catch (ErrorResponse ex)
            {
                return BadRequest(ex.Error);
            }
        }

        /// <summary>
        /// Update Bus
        /// </summary>
        [HttpPut("{ticketId}")]
        public async Task<ActionResult<BaseResponseViewModel<TicketInformationResponse>>> UpdateTicket
            ([FromRoute] int ticketId, [FromBody] UpdateTicketRequest request)
        {
            try
            {
                return await _ticketService.UpdateTicket(ticketId, request);
            }
            catch (ErrorResponse ex)
            {
                return BadRequest(ex.Error);
            }
        }
    }
}
