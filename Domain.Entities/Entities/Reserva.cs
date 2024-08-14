
namespace Domain.Core.Entities
{
    public class Reserva
    {
        public int ReservaID { get; set; }
        public int HabitacionID { get; set; }
        public Habitacion Habitacion { get; set; }
        public DateTime FechaEntrada { get; set; }
        public DateTime FechaSalida { get; set; }
        public int CantidadPersonas { get; set; }
        public string EstadoReserva { get; set; }
        public DateTime FechaReserva { get; set; } = DateTime.UtcNow;
        public DateTime FechaModificacion { get; set; } = DateTime.UtcNow;
        public ICollection<Huesped> Huespedes { get; set; }
        public ContactoEmergencia ContactoEmergencia { get; set; }
    }
}
