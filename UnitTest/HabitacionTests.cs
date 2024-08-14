using Application.Contracts.Contracts;
using Application.Core.Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace UltraGroup.Controllers.Tests
{
    public class HabitacionTests
    {
        private readonly Mock<IHabitacionServicio> _habitacionServicioMock;
        private readonly HabitacionController _controller;
        private readonly ILogger<HabitacionController> _logger;

        public HabitacionTests()
        {
            _habitacionServicioMock = new Mock<IHabitacionServicio>();

            _controller = new HabitacionController(_habitacionServicioMock.Object,_logger);
        }

        [Fact]
        public void CrearHabitacion_DeberiaRetornarHabitacionDTO_Exitoso()
        {
            var habitacionDto = new HabitacionDTO { HabitacionID = 1, Numero = "Habitación Test" };
            var resultadoOperacion = new ResultadoOperacion<HabitacionDTO>
            {
                Exito = true,
                Datos = habitacionDto
            };

            _habitacionServicioMock.Setup(service => service.CrearHabitacion(habitacionDto)).Returns(resultadoOperacion);

            var result = _controller.CrearHabitacion(habitacionDto) as ActionResult<ResultadoOperacion<HabitacionDTO>>;

            var okResult = result.Result as OkObjectResult;
            var returnedDto = okResult?.Value as ResultadoOperacion<HabitacionDTO>;

            Assert.NotNull(returnedDto);
            Assert.True(returnedDto.Exito);
            Assert.Equal(habitacionDto.HabitacionID, returnedDto.Datos.HabitacionID);
        }

        [Fact]
        public void CrearHabitacion_BadRequest_CuandoDTOEsNull()
        {
            var result = _controller.CrearHabitacion(null);

            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.Equal("El objeto habitacionDto no puede ser nulo.", badRequestResult.Value);
        }

        [Fact]
        public void ActualizarHabitacion_DeberiaRetrnarHabitacionDTO_Exitoso()
        {
            var habitacionDto = new HabitacionDTO { HabitacionID = 1, Numero = "Updated Habitacion" };
            var resultadoOperacion = new ResultadoOperacion<HabitacionDTO>
            {
                Exito = true,
                Datos = habitacionDto
            };

            _habitacionServicioMock.Setup(service => service.ActualizarHabitacion(1, habitacionDto)).Returns(resultadoOperacion);

            var result = _controller.ActualizarHabitacion(1, habitacionDto) as ActionResult<ResultadoOperacion<HabitacionDTO>>;

            var okResult = result.Result as OkObjectResult;
            var returnedDto = okResult?.Value as ResultadoOperacion<HabitacionDTO>;

            Assert.NotNull(returnedDto);
            Assert.True(returnedDto.Exito);
            Assert.Equal(habitacionDto.HabitacionID, returnedDto.Datos.HabitacionID);
        }

        [Fact]
        public void ActualizarHabitacion_ShouldReturnBadRequest_WhenIdIsZero()
        {
            var habitacionDto = new HabitacionDTO { HabitacionID = 1, Numero = "Updated Habitacion" };

            var result = _controller.ActualizarHabitacion(0, habitacionDto);

            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.Equal("No existe relacion del id habitación con entidad", badRequestResult.Value);
        }

        [Fact]
        public void ActualizarEstadoHotel_DeberiaRetornarTrue_Exitoso()
        {
            _habitacionServicioMock.Setup(service => service.ActualizarEstadoHabitacion(1, true)).Returns(true);

            var result = _controller.ActualizarEstadoHotel(1, true) as ActionResult<bool>;

            var okResult = result.Result as OkObjectResult;
            var returnedResult = okResult?.Value as bool?;

            Assert.True(returnedResult.HasValue);
            Assert.True(returnedResult.Value);
        }

        [Fact]
        public void ActualizarEstadoHotel_BadRequest_CuandoActualizacionFalle()
        {
            _habitacionServicioMock.Setup(service => service.ActualizarEstadoHabitacion(1, true)).Returns(false);

            var result = _controller.ActualizarEstadoHotel(1, true) as ActionResult<bool>;

            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.Equal("No es posible actualizar", badRequestResult.Value);
        }

        [Fact]
        public void ObtenerHabitacionesPorHotel_DeberiaRetornarHabitacionDTOs()
        {
            var habitaciones = new List<HabitacionDTO>
            {
                new HabitacionDTO { HabitacionID = 1, Numero = "Habitacion 1" },
                new HabitacionDTO { HabitacionID = 2, Numero = "Habitacion 2" }
            };

            _habitacionServicioMock.Setup(service => service.ObtenerHabitacionesPorHotel(1)).Returns(habitaciones);

            var result = _controller.ObtenerHabitacionesPorHotel(1) as ActionResult<IEnumerable<HabitacionDTO>>;

            var okResult = result.Result as OkObjectResult;
            var returnedHabitaciones = okResult?.Value as IEnumerable<HabitacionDTO>;

            Assert.NotNull(returnedHabitaciones);
            Assert.Equal(2, returnedHabitaciones.Count());
        }
    }
}
