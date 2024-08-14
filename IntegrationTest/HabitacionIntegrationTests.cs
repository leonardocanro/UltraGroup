using Application.Core.Class;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Xunit;

namespace UltraGroup.Controllers.IntegrationTests
{
    public class HabitacionIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public HabitacionIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;

            _client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    // Configura servicios aadicionales para pruebas si es necesario
                });
            }).CreateClient();

            // Configura el token de autenticacion para las pruebasss
            var token = "your-test-token"; // Reemplaza con un token válido para pruebas
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        [Fact]
        public async Task CrearHabitacion_DeberiaRetornarHabitacionDTO_Exito()
        {
            var habitacionDto = new HabitacionDTO { HabitacionID = 1, Numero = "Habitación Test" };

            var response = await _client.PostAsJsonAsync("/api/habitacion", habitacionDto);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ResultadoOperacion<HabitacionDTO>>();

            Assert.NotNull(result);
            Assert.True(result.Exito);
            Assert.Equal(habitacionDto.HabitacionID, result.Datos.HabitacionID);
        }

        [Fact]
        public async Task CrearHabitacion_DeberiaRetornarBadRequest_CuandoDtoEsNull()
        {
            var response = await _client.PostAsJsonAsync("/api/habitacion", (HabitacionDTO)null);

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);

            var errorMessage = await response.Content.ReadAsStringAsync();
            Assert.Equal("El objeto habitacionDto no puede ser nulo.", errorMessage);
        }

        [Fact]
        public async Task ActualizarHabitacion_DeberiaRetornarHabitacionDTO_Exito()
        {
            var habitacionDto = new HabitacionDTO { HabitacionID = 1, Numero = "Habitacion Actualizada" };

            var response = await _client.PutAsJsonAsync("/api/habitacion/1", habitacionDto);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ResultadoOperacion<HabitacionDTO>>();

            Assert.NotNull(result);
            Assert.True(result.Exito);
            Assert.Equal(habitacionDto.HabitacionID, result.Datos.HabitacionID);
        }

        [Fact]
        public async Task ActualizarHabitacion_DeberiaRetornarBadRequest_CuandoIdEsZero()
        {
            var habitacionDto = new HabitacionDTO { HabitacionID = 1, Numero = "Updated Habitacion" };

            var response = await _client.PutAsJsonAsync("/api/habitacion/0", habitacionDto);

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);

            var errorMessage = await response.Content.ReadAsStringAsync();
            Assert.Equal("No existe relacion del id habitación con entidad", errorMessage);
        }

        [Fact]
        public async Task ActualizarEstadoHotel_DeberiaRetornarTrue_Exitoso()
        {
            var response = await _client.PatchAsync("/api/habitacion/1/status?isActive=true", null);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            Assert.Equal("true", result);
        }

        [Fact]
        public async Task ActualizarEstadoHotel_DeberiaRetornarBadRequest_WhenActualizacionFalle()
        {
            // Simular un fallo en la actualización configurando el servicio de prueba

            var response = await _client.PatchAsync("/api/habitacion/1/status?isActive=true", null);

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);

            var errorMessage = await response.Content.ReadAsStringAsync();
            Assert.Equal("No es posible actualizar", errorMessage);
        }

        [Fact]
        public async Task ObtenerHabitacionesPorHotel_DeberiaRetornarHabitacionDTOs()
        {
            var response = await _client.GetAsync("/api/habitacion/hotel/1");

            response.EnsureSuccessStatusCode();

            var habitaciones = await response.Content.ReadFromJsonAsync<IEnumerable<HabitacionDTO>>();

            Assert.NotNull(habitaciones);
            Assert.NotEmpty(habitaciones);
        }
    }
}
