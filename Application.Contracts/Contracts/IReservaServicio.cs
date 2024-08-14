using Application.Core.Class;

namespace Application.Contracts.Contracts
{
    public interface IReservaServicio
    {
        IEnumerable<ReservaDTO> ObtenerReservasPorHotel(int hotelId);
        ReservaDetalladaDTO ObtenerReservaDetallada(int reservaId);
        ResultadoOperacion<CrearReservaDTO> CrearReserva(CrearReservaDTO reservaDto);
    }
}
