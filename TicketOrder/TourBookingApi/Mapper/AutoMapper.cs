using AutoMapper;
using BusinessObject.Models;
using DataAccess.DTO.Request.Class;
using DataAccess.DTO.Request.Order;
using DataAccess.DTO.Request.Passenger;
using DataAccess.DTO.Request.Ticket;
using DataAccess.DTO.Response;

namespace TicketOrderApi.Mapper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            #region Class
            CreateMap<Class, ClassResponse>().ReverseMap();
            CreateMap<CreateClassRequest, Class>();
            CreateMap<UpdateClassRequest, Class>();
            #endregion
            #region Order
            CreateMap<Order, OrderResponse>().ReverseMap();
            CreateMap<CreateOrderRequest, Order>();
            CreateMap<UpdateOrderRequest, Order>();
            #endregion
            #region Passenger
            CreateMap<Passenger, PassengerResponse>().ReverseMap();
            CreateMap<CreatePassengerRequest, Passenger>();
            CreateMap<UpdatePassengerRequest, Passenger>();
            #endregion
            #region Ticket
            CreateMap<TicketInformation, TicketInformationResponse>().ReverseMap();
            CreateMap<CreateTicketRequest, TicketInformation>();
            CreateMap<UpdateTicketRequest, TicketInformation>();
            #endregion
            
        }
    }
}
