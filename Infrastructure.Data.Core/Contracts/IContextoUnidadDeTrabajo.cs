using Domain.Core;
using Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Core.Contracts
{
    public interface IContextoUnidadDeTrabajo:IUnidadDeTrabajo,IDisposable
    {
        DbSet<Hotel> Hotel  { get; }
        DbSet<Entidad> Set<Entidad>() where Entidad : class;
        void Attach<Entidad>(Entidad item) where Entidad : class;
        void SetModified<Entidad>(Entidad item) where Entidad: class;
        void GuardarCambios();
    }
}
