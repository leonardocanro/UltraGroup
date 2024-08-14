using Application.Contracts.Contracts;
using Application.Core.Class;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UltraGroup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Requiere autenticación
    public class HotelController : ControllerBase
    {
        private readonly IHotelServicio _hotelServicio;
        public HotelController(IHotelServicio hotelServicio)
        {
            _hotelServicio = hotelServicio;
        }
        [ProducesResponseType(typeof(HotelDTO), 200)]
        [ProducesResponseType(400)]
        [HttpGet("{id}")]
        public ActionResult<HotelDTO> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest("El objeto id no puede ser cero.");
            }
            return _hotelServicio.Obtener(id);
        }
        /// <summary>
        /// Crea un nuevo hotel
        /// </summary>
        // Criterio: El sistema deberá permitir crear un nuevo hotel
        [HttpPost]
        [ProducesResponseType(typeof(HotelDTO), 200)]
        [ProducesResponseType(400)]
        public ActionResult<HotelDTO> CrearHotel(HotelDTO hotelDto)
        {
            if (hotelDto == null)
            {
                return BadRequest("El objeto hotelDto no puede ser nulo.");
            }
            var createdHotel = _hotelServicio.CrearHotel(hotelDto);
            return createdHotel;
        }
        // Criterio: El sistema deberá permitir modificar los valores de cada hotel
        [HttpPut("{id}")]
        public ActionResult<ResultadoOperacion<HotelDTO>> ActualizarHotel(int id, HotelDTO hotelDto)
        {
            if (id != hotelDto.HotelID)
            {
                return BadRequest("No existe relacion del id hotel con entidad");
            }
            var respuesta = _hotelServicio.ActualizarHotel(id, hotelDto);
            if (!respuesta.Exito)
                return BadRequest(respuesta.MensajeError);
            return respuesta;
        }
        /// <summary>
        /// Método para actualizar estado
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        // Criterio: El sistema me deberá permitir habilitar o deshabilitar cada uno de los hoteles
        [HttpPatch("{id}/status")]
        public ActionResult<bool> ActualizarEstadoHotel(int id, [FromQuery] bool isActive)
        {
            var result = _hotelServicio.ActualizarEstadoHotel(id, isActive);
            if (!result)
            {
                return BadRequest("No es posible actualizar");
            }
            return true;
        }
        // Criterio: Buscar hoteles
        [HttpGet]
        public ActionResult<IEnumerable<HotelDTO>> BuscarHoteles(
            [FromQuery] DateTime fechaEntrada,
            [FromQuery] DateTime fechaSalida,
            [FromQuery] int cantidadPersonas,
            [FromQuery] string ciudadDestino)
        {
            var hoteles = _hotelServicio.BuscarHoteles(fechaEntrada, fechaSalida, cantidadPersonas, ciudadDestino);
            return Ok(hoteles);
        }
    }
}
