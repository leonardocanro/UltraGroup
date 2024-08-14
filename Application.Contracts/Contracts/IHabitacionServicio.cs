using Application.Core.Class;

namespace Application.Contracts.Contracts
{
    public interface IHabitacionServicio:IDisposable
    {
        HabitacionDTO Obtener(int id);
        ResultadoOperacion<HabitacionDTO> CrearHabitacion(HabitacionDTO habitacionDto);
        ResultadoOperacion<HabitacionDTO> ActualizarHabitacion(int id, HabitacionDTO habitacionDto);
        bool ActualizarEstadoHabitacion(int id, bool isActive);
        IEnumerable<HabitacionDTO> ObtenerHabitacionesPorHotel(int hotelId);

    }
}
