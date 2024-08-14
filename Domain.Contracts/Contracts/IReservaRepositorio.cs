using Application.Core.Class;
using Domain.Core.Aggregates;
using Domain.Core.Entities;

namespace Domain.Contracts.Contracts
{
    public interface IReservaRepositorio : IRepositorioBase<Reserva>, IDisposable
    {
    }
}
