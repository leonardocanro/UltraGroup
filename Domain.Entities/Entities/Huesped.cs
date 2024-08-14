

namespace Domain.Core.Entities
{
    public class Huesped
    {
        public int HuespedID { get; set; }
        public int ReservaID { get; set; }
        public Reserva Reserva { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public char Genero { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Email { get; set; }
        public string TelefonoContacto { get; set; }
    }
}
