using Application.Core.Class;
using Application.Implementation.Class;
using AutoMapper;
using Domain.Contracts.Contracts;
using Domain.Core.Entities;
using Moq;
using System.Linq.Expressions;
using Xunit;

public class HotelTests
{
    private readonly Mock<IHotelRepositorio> _hotelRepositorioMock;
    private readonly IMapper _mapper;
    private readonly HotelServicio _hotelServicio;

    public HotelTests()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Hotel, HotelDTO>().ReverseMap();
        });
        _mapper = mapperConfig.CreateMapper();

        _hotelRepositorioMock = new Mock<IHotelRepositorio>();
        _hotelServicio = new HotelServicio(_hotelRepositorioMock.Object, _mapper);
    }

    [Fact]
    public void CrearHotel_DeberiaRetornarHotelDTO()
    {
        var hotelDto = new HotelDTO { HotelID = 1, Nombre = "Hotel Test" };
        var hotel = new Hotel { HotelID = 1, Nombre = "Hotel Test" };

        _hotelRepositorioMock.Setup(repo => repo.Agregar(It.IsAny<Hotel>())).Verifiable();
        var result = _hotelServicio.CrearHotel(hotelDto);

        _hotelRepositorioMock.Verify(repo => repo.Agregar(It.IsAny<Hotel>()), Times.Once);
        Assert.NotNull(result);
        Assert.Equal(hotelDto.HotelID, result.HotelID);
        Assert.Equal(hotelDto.Nombre, result.Nombre);
    }

    [Fact]
    public void ActualizarHotel_DeberiaRetornarResultadoOperacion()
    {
        var hotelDto = new HotelDTO { HotelID = 1, Nombre = "Updated Hotel" };
        var hotel = new Hotel { HotelID = 1, Nombre = "Old Hotel" };

        _hotelRepositorioMock.Setup(repo => repo.Obtener(1)).Returns(hotel);
        _hotelRepositorioMock.Setup(repo => repo.Actualizar(It.IsAny<Hotel>())).Verifiable();
        var result = _hotelServicio.ActualizarHotel(1, hotelDto);

        _hotelRepositorioMock.Verify(repo => repo.Actualizar(It.IsAny<Hotel>()), Times.Once);
        Assert.True(result.Exito);
        Assert.Equal(hotelDto.HotelID, result.Datos.HotelID);
        Assert.Equal(hotelDto.Nombre, result.Datos.Nombre);
    }

    [Fact]
    public void ActualizarEstadoHotel_DeberiaRetornarTrue()
    {
        var hotel = new Hotel { HotelID = 1, Activo = false };

        _hotelRepositorioMock.Setup(repo => repo.Obtener(1)).Returns(hotel);
        _hotelRepositorioMock.Setup(repo => repo.Actualizar(It.IsAny<Hotel>())).Verifiable();

        var result = _hotelServicio.ActualizarEstadoHotel(1, true);

        _hotelRepositorioMock.Verify(repo => repo.Actualizar(It.IsAny<Hotel>()), Times.Once);
        Assert.True(result);
        Assert.True(hotel.Activo);
    }

    [Fact]
    public void BuscarHoteles_DeberiaRetornarDTOs()
    {
        var hotel = new Hotel
        {
            HotelID = 1,
            Ciudad = "Bogota",
            Habitaciones = new List<Habitacion>
        {
            new Habitacion
            {
                Reservas = new List<Reserva>
                {
                    new Reserva
                    {
                        FechaEntrada = DateTime.Now.AddDays(-1),
                        FechaSalida = DateTime.Now.AddDays(1),
                        CantidadPersonas = 2
                    }
                }
            }
        }
        };

        var hotelDtos = new List<HotelDTO> { new HotelDTO { HotelID = 1, Ciudad = "Bogota" } };

        var hotels = new List<Hotel> { hotel }.AsQueryable();  // Convertir List a IQueryable

        _hotelRepositorioMock.Setup(repo => repo.Buscarinclude(It.IsAny<Expression<Func<Hotel, bool>>>())).Returns(hotels);

        var result = _hotelServicio.BuscarHoteles(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), 2, "Bogota");

        Assert.NotEmpty(result);
        Assert.Single(result);
        Assert.Equal("Bogota", result.First().Ciudad);
    }

}
