using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessObject.Models;
using BusinessObject.UnitOfWork;
using DataAccess.Attributes;
using DataAccess.DTO.Request;
using DataAccess.DTO.Request.Ticket;
using DataAccess.DTO.Response;
using DataAccess.Exceptions;
using DataAccess.Helpers;
using DataAccess.Utilities;

namespace DataAccess.Services
{
    public interface ITicketServices
    {
        Task<BaseResponsePagingViewModel<TicketInformationResponse>> GetAllTicket(PagingRequest paging);
        Task<BaseResponseViewModel<TicketInformationResponse>> CreateTicket(CreateTicketRequest request);
        Task<BaseResponseViewModel<TicketInformationResponse>> UpdateTicket(int ticketId, UpdateTicketRequest request);
    }

    public class TicketServices : ITicketServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TicketServices(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task<BaseResponsePagingViewModel<TicketInformationResponse>> GetAllTicket(PagingRequest paging)
        {
            try
            {
                {
                    var ticket = _unitOfWork.Repository<TicketInformation>().GetAll()
                        .ProjectTo<TicketInformationResponse>(_mapper.ConfigurationProvider)
                        .PagingQueryable(paging.Page, paging.PageSize, Constants.LimitPaging,
                            Constants.DefaultPaging);
                    return new BaseResponsePagingViewModel<TicketInformationResponse>()
                    {
                        Metadata = new PagingsMetadata()
                        {
                            Page = paging.Page,
                            Size = paging.PageSize,
                            Total = ticket.Item1
                        },
                        Data = ticket.Item2.ToList()
                    };
                }
            }
            catch (ErrorResponse ex)
            {
                throw;
            }
        }

        public async Task<BaseResponseViewModel<TicketInformationResponse>> CreateTicket(CreateTicketRequest request)
        {
            var ticket = _mapper.Map<CreateTicketRequest, TicketInformation>(request);

            await _unitOfWork.Repository<TicketInformation>().InsertAsync(ticket);
            await _unitOfWork.CommitAsync();
            return new BaseResponseViewModel<TicketInformationResponse>()
            {
                Status = new StatusViewModel()
                {
                    Message = "Success",
                    Success = true,
                    ErrorCode = 0
                },
                Data = _mapper.Map<TicketInformationResponse>(ticket)
            };
        }

        public async Task<BaseResponseViewModel<TicketInformationResponse>> UpdateTicket(int ticketId, UpdateTicketRequest request)
        {
            var ticket = _unitOfWork.Repository<TicketInformation>().GetAll()
                .FirstOrDefault(x => x.Id == ticketId);

            if (ticket == null)
                throw new ErrorResponse(404, (int)ErrorEnum.BusErrorEnums.NOT_FOUND,
                    ErrorEnum.BusErrorEnums.NOT_FOUND.GetDisplayName());

            var updateTicket = _mapper.Map<UpdateTicketRequest, TicketInformation>(request, ticket);


            await _unitOfWork.Repository<TicketInformation>().UpdateDetached(updateTicket);
            await _unitOfWork.CommitAsync();

            return new BaseResponseViewModel<TicketInformationResponse>()
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