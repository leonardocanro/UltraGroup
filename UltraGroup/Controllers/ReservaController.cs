using Application.Contracts.Contracts;
using Application.Core.Class;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UltraGroup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Requiere autenticación
    public class ReservaController : ControllerBase
    {
        private readonly IReservaServicio _reservaServicio;
        public ReservaController(IReservaServicio reservaServicio)
        {
            _reservaServicio = reservaServicio;
        }
        // Criterio: El sistema me deberá permitir listar cada una de las reservas realizadas en mis hoteles
        [HttpGet("{id}/reservas")]
        public ActionResult<IEnumerable<ReservaDTO>> ObtenerReservasPorHotel(int id)
        {
            var reservas = _reservaServicio.ObtenerReservasPorHotel(id);
            return Ok(reservas);
        }
        // Criterio: El sistema me deberá permitir ver el detalle de cada una de las reservas realizadas
        [HttpGet("reserva/{reservaId}")]
        public ActionResult<ReservaDetalladaDTO> ObtenerReservaDetallada(int reservaId)
        {
            var reserva = _reservaServicio.ObtenerReservaDetallada(reservaId);
            if (reserva == null)
            {
                return BadRequest();
            }
            return reserva;
        }
        // Criterio: Crear reserva
        [HttpPost]
        public ActionResult<ResultadoOperacion<CrearReservaDTO>> CrearReserva(CrearReservaDTO reservaDto)
        {
            var respuesta = _reservaServicio.CrearReserva(reservaDto);
            if (!respuesta.Exito)
                return BadRequest(respuesta.MensajeError);
            return respuesta;
        }
    }
}
