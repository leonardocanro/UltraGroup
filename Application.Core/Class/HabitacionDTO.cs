using Domain.Core.Entities;

namespace Application.Core.Class
{
    public class HabitacionDTO
    {
        public int HabitacionID { get; set; }
        public int HotelID { get; set; }
        public string Numero { get; set; }
        public decimal CostoBase { get; set; }
        public decimal Impuestos { get; set; }
        public string Tipo { get; set; }
        public string Ubicacion { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public DateTime FechaModificacion { get; set; } = DateTime.UtcNow;
    }
}
