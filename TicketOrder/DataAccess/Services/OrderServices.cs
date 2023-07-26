using System.Net.Http.Json;
using System.Text.Json;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessObject.Models;
using BusinessObject.UnitOfWork;
using DataAccess.Attributes;
using DataAccess.DTO.Request;
using DataAccess.DTO.Request.Order;
using DataAccess.DTO.Request.Ticket;
using DataAccess.DTO.Response;
using DataAccess.Exceptions;
using DataAccess.Helpers;
using DataAccess.Utilities;


namespace DataAccess.Services
{
    public interface IOrderServices
    {
        Task<BaseResponsePagingViewModel<OrderResponse>> GetAllOrder(PagingRequest paging);
        Task<BaseResponseViewModel<OrderResponse>> CreateOrder(CreateOrderRequest request);
        Task<BaseResponseViewModel<OrderResponse>> UpdateOrder(int orderId, UpdateOrderRequest request);
    }

    public class OrderServices : IOrderServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpClientFactory _httpClientFactory;

        public OrderServices(IMapper mapper, IUnitOfWork unitOfWork, IHttpClientFactory httpClientFactory)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<BaseResponsePagingViewModel<OrderResponse>> GetAllOrder(PagingRequest paging)
        {
            try
            {
                {
                    var order = _unitOfWork.Repository<Order>().GetAll()
                        .ProjectTo<OrderResponse>(_mapper.ConfigurationProvider)
                        .PagingQueryable(paging.Page, paging.PageSize, Constants.LimitPaging,
                            Constants.DefaultPaging);
                    return new BaseResponsePagingViewModel<OrderResponse>()
                    {
                        Metadata = new PagingsMetadata()
                        {
                            Page = paging.Page,
                            Size = paging.PageSize,
                            Total = order.Item1
                        },
                        Data = order.Item2.ToList()
                    };
                }
            }
            catch (ErrorResponse ex)
            {
                throw;
            }
        }

        // public TicketResponse? TicketData { get; set; }

        public async Task<BaseResponseViewModel<OrderResponse>> CreateOrder(CreateOrderRequest request)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var ticketData =
                await httpClient.GetFromJsonAsync<CreateTicketRequest>(
                    $"https://ticket-service:80/api/Ticket/{request.TicketId}");
            TicketOrder ordticket = new TicketOrder();
            var order = _mapper.Map<CreateOrderRequest, Order>(request);
            if (ticketData?.AvailableTicket < request.Quantity)
            {
                throw  new ErrorResponse(400, 400, "bad request");
            }
            if (ticketData != null)
            {
                ticketData.Quantity = request.Quantity;   
                await _unitOfWork.Repository<Order>().InsertAsync(order);
                await _unitOfWork.CommitAsync();
                var ticketInfo = _mapper.Map<CreateTicketRequest, TicketInformation>(ticketData);
                await _unitOfWork.Repository<TicketInformation>().InsertAsync(ticketInfo);
                await _unitOfWork.CommitAsync();
                
                ordticket.OrderId = order.Id;
                ordticket.TicketId = ticketInfo.Id;
                await _unitOfWork.Repository<TicketOrder>().InsertAsync(ordticket);
                await _unitOfWork.CommitAsync();
            }

            ticketData.AvailableTicket -= request.Quantity;
            await httpClient.PutAsJsonAsync<CreateTicketRequest>(
                $"https://ticket-service:80/api/Ticket/{request.TicketId}", ticketData);
            return new BaseResponseViewModel<OrderResponse>()
            {
                Status = new StatusViewModel()
                {
                    Message = "Success",
                    Success = true,
                    ErrorCode = 0
                },
                Data = _mapper.Map<OrderResponse>(order)
            };
        }

        public async Task<BaseResponseViewModel<OrderResponse>> UpdateOrder(int orderId, UpdateOrderRequest request)
        {
            var order = _unitOfWork.Repository<Order>().GetAll()
                .FirstOrDefault(x => x.Id == orderId);

            if (order == null)
                throw new ErrorResponse(404, (int)ErrorEnum.BusErrorEnums.NOT_FOUND,
                    ErrorEnum.BusErrorEnums.NOT_FOUND.GetDisplayName());

            var updateOrders = _mapper.Map<UpdateOrderRequest, Order>(request, order);


            await _unitOfWork.Repository<Order>().UpdateDetached(updateOrders);
            await _unitOfWork.CommitAsync();

            return new BaseResponseViewModel<OrderResponse>()
            {
                Status = new StatusViewModel()
                {
                    Message = "Success",
                    Success = true,
                    ErrorCode = 0
                }
            };
        }
    }
}