using Application.Contracts.Contracts;
using Application.Core.Class;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UltraGroup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize] // Requiere autenticación
    public class HabitacionController : ControllerBase
    {
        private readonly IHabitacionServicio _habitacionServicio;
        private readonly ILogger<HabitacionController> _logger;
        public HabitacionController(IHabitacionServicio habitacionServicio, ILogger<HabitacionController> logger)
        {
            _habitacionServicio = habitacionServicio;
            _logger = logger;
        }
        /// <summary>
        /// Crea un nuevo habitacion
        /// </summary>
        // Criterio: El sistema deberá permitir asignar al hotel cada una de las habitaciones disponibles para reserva
        [HttpPost]
        [ProducesResponseType(typeof(HabitacionDTO), 200)]
        [ProducesResponseType(400)]
        public ActionResult<ResultadoOperacion<HabitacionDTO>> CrearHabitacion(HabitacionDTO habitacionDto)
        {
            if (habitacionDto == null)
            {
                return BadRequest("El objeto habitacionDto no puede ser nulo.");
            }
            var respuesta = _habitacionServicio.CrearHabitacion(habitacionDto);
            if (!respuesta.Exito)
                return BadRequest(respuesta.MensajeError);
            return respuesta;
        }
        // Criterio: El sistema deberá permitir modificar los valores de cada habitación
        [HttpPut("{id}")]
        public ActionResult<ResultadoOperacion<HabitacionDTO>> ActualizarHabitacion(int id, HabitacionDTO habitacionDto)
        {
            if (id == 0)
            {
                return BadRequest("No existe relacion del id habitación con entidad");
            }
            var respuesta = _habitacionServicio.ActualizarHabitacion(id, habitacionDto);
            if (!respuesta.Exito)
                return BadRequest(respuesta.MensajeError);
            return respuesta;
        }
        // Criterio: El sistema me deberá permitir habilitar o deshabilitar cada una de las habitaciones del hotel
        [HttpPatch("{id}/status")]
        public ActionResult<bool> ActualizarEstadoHotel(int id, [FromQuery] bool isActive)
        {
            var result = _habitacionServicio.ActualizarEstadoHabitacion(id, isActive);
            if (!result)
            {
                return BadRequest("No es posible actualizar");
            }
            return true;
        }
        // Criterio: Cada habitación deberá permitir registrar el costo base, impuestos y tipo de habitación
        // Criterio: Cada habitación deberá permitir registrar la ubicación en que se encuentra
        [HttpGet("hotel/{hotelId}")]
        public ActionResult<IEnumerable<HabitacionDTO>> ObtenerHabitacionesPorHotel(int hotelId)
        {
            _logger.LogInformation("Hola");
            var habitaciones = _habitacionServicio.ObtenerHabitacionesPorHotel(hotelId);
            _logger.LogInformation("Habitaciones del hotelId {hotelId} obtenidas con éxito: {habitaciones} ",hotelId, habitaciones);
            return Ok(habitaciones);
        }
    }
}
