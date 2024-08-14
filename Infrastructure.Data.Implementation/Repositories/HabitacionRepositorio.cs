using Domain.Contracts.Contracts;
using Domain.Core.Entities;
using Infrastructure.Data.Core.Contracts;
using Infrastructure.Data.Repositories.Class;

namespace Infrastructure.Data.Implementation.Repositories
{
    public class HabitacionRepositorio : RepositorioBase<Habitacion>, IHabitacionRepositorio
    {
        public HabitacionRepositorio(IContextoUnidadDeTrabajo unidadDeTrabajo) : base(unidadDeTrabajo) { }
    }
}
