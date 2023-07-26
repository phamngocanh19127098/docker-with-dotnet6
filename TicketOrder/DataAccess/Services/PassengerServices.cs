using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessObject.Models;
using BusinessObject.UnitOfWork;
using DataAccess.Attributes;
using DataAccess.DTO.Request;
using DataAccess.DTO.Request.Passenger;
using DataAccess.DTO.Response;
using DataAccess.Exceptions;
using DataAccess.Helpers;
using DataAccess.Utilities;

namespace DataAccess.Services

{
    public interface IPassengerServices
    {
        Task<BaseResponsePagingViewModel<PassengerResponse>> GetAllPassenger(PagingRequest paging);
        Task<BaseResponseViewModel<PassengerResponse>> CreatePassenger(CreatePassengerRequest request);
        Task<BaseResponseViewModel<PassengerResponse>> UpdatePassenger(int passengerId, UpdatePassengerRequest request);
    }
    
    public class PassengerServices : IPassengerServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PassengerServices(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task<BaseResponsePagingViewModel<PassengerResponse>> GetAllPassenger(PagingRequest paging)
        {
            try
            {

                {
                    var passenger = _unitOfWork.Repository<Passenger>().GetAll()
                        .ProjectTo<PassengerResponse>(_mapper.ConfigurationProvider)
                        .PagingQueryable(paging.Page, paging.PageSize, Constants.LimitPaging,
                            Constants.DefaultPaging);
                    return new BaseResponsePagingViewModel<PassengerResponse>()
                    {
                        Metadata = new PagingsMetadata()
                        {
                            Page = paging.Page,
                            Size = paging.PageSize,
                            Total = passenger.Item1
                        },
                        Data = passenger.Item2.ToList()
                    };
                }
            }
            catch (ErrorResponse ex)
            {
                throw;
            }
        }

        public async Task<BaseResponseViewModel<PassengerResponse>> CreatePassenger(CreatePassengerRequest request)
        {
            var passenger = _mapper.Map<CreatePassengerRequest, Passenger>(request);

            await _unitOfWork.Repository<Passenger>().InsertAsync(passenger);
            await _unitOfWork.CommitAsync();
            return new BaseResponseViewModel<PassengerResponse>()
            {
                Status = new StatusViewModel()
                {
                    Message = "Success",
                    Success = true,
                    ErrorCode = 0
                },
                Data = _mapper.Map<PassengerResponse>(passenger)
            };
        }

        public async Task<BaseResponseViewModel<PassengerResponse>> UpdatePassenger(int passengerId, UpdatePassengerRequest request)
        {
            var passenger = _unitOfWork.Repository<Passenger>().GetAll()
                .FirstOrDefault(x => x.Id == passengerId);

            if (passenger == null)
                throw new ErrorResponse(404, (int)ErrorEnum.BusErrorEnums.NOT_FOUND,
                    ErrorEnum.BusErrorEnums.NOT_FOUND.GetDisplayName());

            var updatePassenger = _mapper.Map<UpdatePassengerRequest, Passenger>(request, passenger);


            await _unitOfWork.Repository<Passenger>().UpdateDetached(updatePassenger);
            await _unitOfWork.CommitAsync();

            return new BaseResponseViewModel<PassengerResponse>()
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