using System.Linq.Expressions;


namespace Domain.Core.Aggregates
{
    public interface IRepositorioBase<Entidad> : IDisposable
    {
        // Define las propiedades para los repositorios que necesitamos
        IUnidadDeTrabajo UnidadDeTrabajo { get; }
        Entidad Obtener(int id);
        IEnumerable<Entidad> ObtenerTodas();
        IEnumerable<Entidad> Buscar(Expression<Func<Entidad, bool>> predicado);
        IQueryable<Entidad> Buscarinclude(Expression<Func<Entidad, bool>> predicado);
        Entidad BuscarSingleOrDefault(Expression<Func<Entidad, bool>> predicado);
        // Método para guardar los cambios en la base de datos
        void Agregar(Entidad entidad);
        void Actualizar(Entidad entidad);
        void Eliminar(Entidad entidad);
    }
}
