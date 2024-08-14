﻿namespace Application.Core.Class
{
    public class ReservaDTO
    {
        public int ReservaID { get; set; }
        public int HabitacionID { get; set; }
        public HabitacionDTO Habitacion { get; set; }
        public DateTime FechaEntrada { get; set; }
        public DateTime FechaSalida { get; set; }
        public int CantidadPersonas { get; set; }
        public string EstadoReserva { get; set; }
        public DateTime FechaReserva { get; set; } = DateTime.UtcNow;
        public DateTime FechaModificacion { get; set; } = DateTime.UtcNow;
    }
}
