using Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Class
{
    public class ContactoEmergenciaDTO
    {
        public int ContactoEmergenciaID { get; set; }
        public int ReservaID { get; set; }
        public string NombreCompleto { get; set; }
        public string TelefonoContacto { get; set; }
    }
}
