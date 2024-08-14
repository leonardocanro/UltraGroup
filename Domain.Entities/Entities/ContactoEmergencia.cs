

namespace Domain.Core.Entities
{
    public class ContactoEmergencia
    {
        public int ContactoEmergenciaID { get; set; }
        public int ReservaID { get; set; }
        public Reserva Reserva { get; set; }
        public string NombreCompleto { get; set; }
        public string TelefonoContacto { get; set; }
    }
}
