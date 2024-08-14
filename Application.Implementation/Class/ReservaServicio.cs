using Application.Contracts.Contracts;
using Application.Core.Class;
using AutoMapper;
using Domain.Contracts.Contracts;
using Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net.Mail;

namespace Application.Implementation.Class
{
    public class ReservaServicio : IReservaServicio
    {
        private readonly IMapper _mapper;
        private IHabitacionRepositorio _habitacionRepositorio;
        private IHotelRepositorio _hotelRepositorio;
        private IReservaRepositorio _reservaRepositorio;
        public ReservaServicio(IHabitacionRepositorio habitacionRepositorio, IMapper mapper, IHotelRepositorio hotelRepositorio, IReservaRepositorio reservaRepositorio)
        {
            _habitacionRepositorio = habitacionRepositorio;
            _mapper = mapper;
            _hotelRepositorio = hotelRepositorio;
            _reservaRepositorio = reservaRepositorio;
        }
        // Criterio: El sistema me deberá permitir listar cada una de las reservas realizadas en mis hoteles
        public IEnumerable<ReservaDTO> ObtenerReservasPorHotel(int hotelId)
        {
            Expression<Func<Reserva, bool>> predicado = (p => p.Habitacion.Hotel.HotelID == hotelId);
            var reservas = _reservaRepositorio.Buscarinclude(predicado).Include(x => x.Habitacion);

            return _mapper.Map<IEnumerable<ReservaDTO>>(reservas);
        }
        // Criterio: El sistema me deberá permitir ver el detalle de cada una de las reservas realizadas
        public ReservaDetalladaDTO ObtenerReservaDetallada(int reservaId)
        {
            Expression<Func<Reserva, bool>> predicado = (p => p.ReservaID == reservaId);
            var reservas = _reservaRepositorio.Buscarinclude(predicado).Include(x => x.Huespedes).Include(y => y.ContactoEmergencia).Include(z => z.Habitacion).SingleOrDefault();
            return _mapper.Map<ReservaDetalladaDTO>(reservas);
        }
        // Criterio: Crear reserva
        public ResultadoOperacion<CrearReservaDTO> CrearReserva(CrearReservaDTO reservaDto)
        {
            if (reservaDto.Huespedes == null)
            {
                return new ResultadoOperacion<CrearReservaDTO>
                {
                    Exito = false,
                    MensajeError = "La reserva debe tener por lo menos un húesped"
                };
            }
            if (reservaDto.ContactoEmergencia == null || reservaDto.ContactoEmergencia.NombreCompleto == "string")
            {
                return new ResultadoOperacion<CrearReservaDTO>
                {
                    Exito = false,
                    MensajeError = "La reserva debe tener un contacto de emergencia"
                };
            }
            var reserva = _mapper.Map<Reserva>(reservaDto);
            _reservaRepositorio.Agregar(reserva);

            // Enviar notificación por correo electrónico
            EnviarNotificacionCorreo(reservaDto);

            return new ResultadoOperacion<CrearReservaDTO>
            {
                Exito = true,
                Datos = _mapper.Map<CrearReservaDTO>(reserva)
            };
        }
        /// <summary>
        /// Método que envia correo de confirmación de reserva
        /// </summary>
        /// <param name="reservaDto"></param>
        private void EnviarNotificacionCorreo(CrearReservaDTO reservaDto)
        {
            string toMail = reservaDto.Huespedes.First().Email;
            string fromMail = "leonardo.canro@gmail.com";
            var message = new MailMessage(fromMail, toMail);
            message.Subject = "Confirmación de Reserva";
            message.Body = $"Estimado/a {reservaDto.Huespedes.First().Nombre},\n\nSu reserva en el hotel ha sido confirmada.\n\nDetalles de la reserva:\nFecha de entrada: {reservaDto.FechaEntrada}\nFecha de salida: {reservaDto.FechaSalida}\nHabitación: {reservaDto.HabitacionID}\n\n¡Gracias por su preferencia!";
            message.IsBodyHtml = false;

            using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential("leonardo.canro@gmail.com", "hxay igdz mdph okeq");
                smtpClient.Send(message);
            }
        }
    }
}
