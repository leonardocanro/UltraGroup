namespace Application.Core.Class
{
    public class CrearReservaDTO
    {
        public int ReservaID { get; set; }
        public int HabitacionID { get; set; }
        public DateTime FechaEntrada { get; set; }
        public DateTime FechaSalida { get; set; }
        public int CantidadPersonas { get; set; }
        public string EstadoReserva { get; set; }
        public DateTime FechaReserva { get; set; } = DateTime.UtcNow;
        public DateTime FechaModificacion { get; set; } = DateTime.UtcNow;
        public ICollection<HuespedDTO>? Huespedes { get; set; }
        public ContactoEmergenciaDTO? ContactoEmergencia { get; set; }
    }
}
