using Domain.Core;
using Domain.Core.Aggregates;
using Infrastructure.Data.Core.Contracts;
using System.Data.Entity;
using System.Linq.Expressions;


namespace Infrastructure.Data.Repositories.Class
{
    public class RepositorioBase<Entidad> : IRepositorioBase<Entidad> where Entidad : class
    {
        readonly IContextoUnidadDeTrabajo _unidadDeTrabajo;
        public IUnidadDeTrabajo UnidadDeTrabajo
        {
            get { return _unidadDeTrabajo; }
        }
        public RepositorioBase(IContextoUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }
        public Entidad Obtener(int id)
        {
            return _unidadDeTrabajo.Set<Entidad>().Find(id);
        }
        public IEnumerable<Entidad> ObtenerTodas()
        {
            return _unidadDeTrabajo.Set<Entidad>().ToList();
        }
        public IEnumerable<Entidad> Buscar(Expression<Func<Entidad, bool>> predicado)
        {
            return _unidadDeTrabajo.Set<Entidad>().Where(predicado).AsNoTracking();
        }
        public IQueryable<Entidad> Buscarinclude(Expression<Func<Entidad, bool>> predicado)
        {
            return _unidadDeTrabajo.Set<Entidad>().Where(predicado).AsNoTracking();
        }

        public Entidad BuscarSingleOrDefault(Expression<Func<Entidad, bool>> predicado)
        {
            return _unidadDeTrabajo.Set<Entidad>().Single(predicado);
        }

        // Método para guardar los cambios en la base de datos
        public void Agregar(Entidad entidad)
        {
            _unidadDeTrabajo.Set<Entidad>().Add(entidad);
            _unidadDeTrabajo.GuardarCambios();
        }
        public void Actualizar(Entidad entidad)
        {
            _unidadDeTrabajo.SetModified<Entidad>(entidad);
            _unidadDeTrabajo.GuardarCambios();
        }
        public void Eliminar(Entidad entidad)
        {
            _unidadDeTrabajo.Set<Entidad>().Remove(entidad);
        }
        public void Dispose()
        {
            _unidadDeTrabajo.Dispose();
        }
    }
}
