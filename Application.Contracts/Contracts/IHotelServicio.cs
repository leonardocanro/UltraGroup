using Application.Core.Class;

namespace Application.Contracts.Contracts
{
    public interface IHotelServicio : IDisposable
    {
        HotelDTO Obtener(int id);
        HotelDTO CrearHotel(HotelDTO entidad);
        ResultadoOperacion<HotelDTO> ActualizarHotel(int id, HotelDTO hotelDto);
        bool ActualizarEstadoHotel(int id, bool isActive);
        IEnumerable<HotelDTO> BuscarHoteles(DateTime fechaEntrada, DateTime fechaSalida, int cantidadPersonas, string ciudadDestino);
    }
}
