using Application.Contracts.Contracts;
using Application.Core.Class;
using AutoMapper;
using Domain.Contracts.Contracts;
using Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Implementation.Class
{
    public class HotelServicio : IHotelServicio
    {
        private readonly IMapper _mapper;
        private IHotelRepositorio _hotelRepositorio;
        public HotelServicio(IHotelRepositorio hotelRepositorio, IMapper mapper)
        {
            _hotelRepositorio = hotelRepositorio;
            _mapper = mapper;
        }
        // Criterio: El sistema deberá permitir crear un nuevo hotel
        public HotelDTO Obtener(int id)
        {
            var objRecuperado = _hotelRepositorio.Obtener(id);
            return _mapper.Map<Hotel, HotelDTO>(objRecuperado);
        }
        public HotelDTO CrearHotel(HotelDTO hotelDto)
        {
            var hotel = _mapper.Map<Hotel>(hotelDto);
            _hotelRepositorio.Agregar(hotel);
            return _mapper.Map<HotelDTO>(hotel);
        }
        // Criterio: El sistema deberá permitir modificar los valores de cada hotel
        public ResultadoOperacion<HotelDTO> ActualizarHotel(int id, HotelDTO hotelDto)
        {
            var objRecuperado = _hotelRepositorio.Obtener(id);
            if (objRecuperado.HotelID == 0)
            {
                return new ResultadoOperacion<HotelDTO>
                {
                    Exito = false,
                    MensajeError = "Hotel con id " + id + " no encontrado"
                };
            }
            var hotel = _mapper.Map<Hotel>(hotelDto);
            _hotelRepositorio.Actualizar(hotel);
            return new ResultadoOperacion<HotelDTO>
            {
                Exito = true,
                MensajeError = string.Empty,
                Datos = _mapper.Map<HotelDTO>(hotel)
            };
        }
        // Criterio: El sistema me deberá permitir habilitar o deshabilitar cada uno de los hoteles
        public bool ActualizarEstadoHotel(int id, bool isActive)
        {
            var hotel = _hotelRepositorio.Obtener(id);
            if (hotel == null) return false;

            hotel.Activo = isActive;
            _hotelRepositorio.Actualizar(hotel);
            return true;
        }
        //Criterio: El sistema me deberá dar la opción de buscar por:
        //        fecha de entrada al alojamiento, fecha de salida del
        //        alojamiento, cantidad de personas que se alojarán y
        //        ciudad de destino.
        public IEnumerable<HotelDTO> BuscarHoteles(DateTime fechaEntrada, DateTime fechaSalida, int cantidadPersonas, string ciudadDestino)
        {
            Expression<Func<Hotel, bool>> predicado = (p => p.Ciudad == ciudadDestino && p.Habitaciones.
            Any(hab => hab.Reservas.
            Any(res => res.CantidadPersonas >= cantidadPersonas)
            && hab.Reservas.All(x => x.FechaEntrada <= fechaSalida && x.FechaSalida >= fechaEntrada)));
            var hoteles = _hotelRepositorio.Buscarinclude(predicado).Include(h => h.Habitaciones).ToList();

            return hoteles.Select(h => new HotelDTO
            {
                HotelID = h.HotelID,
                Nombre = h.Nombre,
                Ciudad = h.Ciudad,
                Activo = h.Activo,
                Pais = h.Pais,
                Descripcion = h.Descripcion,
                Direccion = h.Direccion,
                Estado = h.Estado
            });
        }
        public void Dispose()
        {

        }
    }
}
