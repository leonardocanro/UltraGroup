using Domain.Contracts.Contracts;
using Domain.Core.Entities;
using Infrastructure.Data.Core.Contracts;
using Infrastructure.Data.Repositories.Class;

namespace Infrastructure.Data.Implementation.Repositories
{
    public class HotelRepositorio : RepositorioBase<Hotel>, IHotelRepositorio
    {
        public HotelRepositorio(IContextoUnidadDeTrabajo unidadDeTrabajo) : base(unidadDeTrabajo) { }
    }
}
