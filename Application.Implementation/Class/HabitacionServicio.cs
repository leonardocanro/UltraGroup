using Application.Contracts.Contracts;
using Application.Core.Class;
using AutoMapper;
using Domain.Contracts.Contracts;
using Domain.Core.Entities;
using System.Linq.Expressions;

namespace Application.Implementation.Class
{
    public class HabitacionServicio : IHabitacionServicio
    {
        private readonly IMapper _mapper;
        private IHabitacionRepositorio _habitacionRepositorio;
        private IHotelRepositorio _hotelRepositorio;
        public HabitacionServicio(IHabitacionRepositorio habitacionRepositorio, IMapper mapper, IHotelRepositorio hotelRepositorio)
        {
            _habitacionRepositorio = habitacionRepositorio;
            _mapper = mapper;
            _hotelRepositorio = hotelRepositorio;
        }
        public HabitacionDTO Obtener(int id)
        {
            var objRecuperado = _habitacionRepositorio.Obtener(id);
            return _mapper.Map<Habitacion, HabitacionDTO>(objRecuperado);
        }
        // Criterio: El sistema deberá permitir asignar al hotel cada una de las habitaciones disponibles para reserva
        public ResultadoOperacion<HabitacionDTO> CrearHabitacion(HabitacionDTO habitacionDto)
        {
            var objRecuperado = _hotelRepositorio.Obtener(habitacionDto.HotelID);
            if (objRecuperado == null)
            {
                return new ResultadoOperacion<HabitacionDTO>
                {
                    Exito = false,
                    MensajeError = "Hotel con id " + habitacionDto.HotelID + " no encontrado"
                };
            }
            var habitacion = _mapper.Map<Habitacion>(habitacionDto);
            _habitacionRepositorio.Agregar(habitacion);
            return new ResultadoOperacion<HabitacionDTO>
            {
                Exito = true,
                Datos = _mapper.Map<HabitacionDTO>(habitacion)
            };
        }
        // Criterio: El sistema deberá permitir modificar los valores de cada habitación
        public ResultadoOperacion<HabitacionDTO> ActualizarHabitacion(int id, HabitacionDTO habitacionDto)
        {
            var objRecuperado = _hotelRepositorio.Obtener(habitacionDto.HotelID);
            if (objRecuperado == null)
                return new ResultadoOperacion<HabitacionDTO>
                {
                    Exito = false,
                    MensajeError = "Hotel con id " + habitacionDto.HotelID + " no encontrado"
                };
            var habitacion = _habitacionRepositorio.Obtener(id);
            if (habitacion == null)
                return new ResultadoOperacion<HabitacionDTO>
                {
                    Exito = false,
                    MensajeError = "Habitación con id " + id + " no encontrado"
                };
            habitacionDto.HabitacionID = habitacion.HabitacionID;
            var habitacionActualizar = _mapper.Map<Habitacion>(habitacionDto);
            _habitacionRepositorio.Actualizar(habitacionActualizar);
            return new ResultadoOperacion<HabitacionDTO>
            {
                Exito = true,
                MensajeError = string.Empty,
                Datos = _mapper.Map<HabitacionDTO>(habitacion)
            };
        }
        // Criterio: El sistema me deberá permitir habilitar o deshabilitar cada una de las habitaciones del hotel
        public bool ActualizarEstadoHabitacion(int id, bool isActive)
        {
            var habitacion = _habitacionRepositorio.Obtener(id);
            if (habitacion == null) return false;

            habitacion.Activo = isActive;
            _habitacionRepositorio.Actualizar(habitacion);
            return true;
        }
        // Criterio: Cada habitación deberá permitir registrar el costo base, impuestos y tipo de habitación
        // (Este criterio está implícito en la creación y actualización de habitaciones)


        // Criterio: Cada habitación deberá permitir registrar la ubicación en que se encuentra
        public IEnumerable<HabitacionDTO> ObtenerHabitacionesPorHotel(int hotelId)
        {
            var objRecuperado = _hotelRepositorio.Obtener(hotelId);
            if (objRecuperado == null)
                return Enumerable.Empty<HabitacionDTO>();
            Expression<Func<Habitacion, bool>> predicado = (p => p.HotelID == hotelId);
            var habitaciones = _habitacionRepositorio.Buscar(predicado);
            return _mapper.Map<IEnumerable<HabitacionDTO>>(habitaciones);
        }

        public void Dispose()
        {

        }
    }
}
