using Application.Core.Class;
using AutoMapper;
using Domain.Core.Entities;


namespace Application.Core.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<Habitacion, HabitacionDTO>().ReverseMap();
            CreateMap<Reserva, ReservaDTO>().ReverseMap();
            CreateMap<Reserva, ReservaDetalladaDTO>().ReverseMap();
            CreateMap<HuespedDTO, ReservaDetalladaDTO>().ReverseMap();
            CreateMap<ContactoEmergenciaDTO, ReservaDetalladaDTO>().ReverseMap();
            CreateMap<HabitacionDTO, ReservaDetalladaDTO>().ReverseMap();
            CreateMap<Huesped, HuespedDTO>().ReverseMap();
            CreateMap<ContactoEmergencia, ContactoEmergenciaDTO>().ReverseMap();
            CreateMap<Reserva, CrearReservaDTO>().ReverseMap();
        }
    }
}
