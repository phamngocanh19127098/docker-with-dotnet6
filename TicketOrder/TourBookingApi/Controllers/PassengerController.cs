using DataAccess.DTO.Request;
using DataAccess.DTO.Request.Class;
using DataAccess.DTO.Request.Passenger;
using DataAccess.DTO.Response;
using DataAccess.Exceptions;
using DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace TicketOrderApi.Controllers
{
[Route(Helpers.SettingVersionApi.ApiVersion)]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly IPassengerServices _passengertService;

        public PassengerController(IPassengerServices passengetService)
        {
            _passengertService = passengetService;
        }

        /// <summary>
        /// Get list bus
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<BaseResponsePagingViewModel<PassengerResponse>>> GetAllPassenger
            ([FromQuery] PassengerResponse request, [FromQuery] PagingRequest paging)
        {
            try
            {
                return await _passengertService.GetAllPassenger(paging);
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
        public async Task<ActionResult<BaseResponseViewModel<PassengerResponse>>> CreatePassenger
            ([FromBody] CreatePassengerRequest request)
        {
            try
            {
                return await _passengertService.CreatePassenger(request);
            }
            catch (ErrorResponse ex)
            {
                return BadRequest(ex.Error);
            }
        }

        /// <summary>
        /// Update Bus
        /// </summary>
        [HttpPut("{passengerId}")]
        public async Task<ActionResult<BaseResponseViewModel<PassengerResponse>>> UpdatePassenger
            ([FromRoute] int passengerId, [FromBody] UpdatePassengerRequest request)
        {
            try
            {
                return await _passengertService.UpdatePassenger(passengerId, request);
            }
            catch (ErrorResponse ex)
            {
                return BadRequest(ex.Error);
            }
        }
    }
}