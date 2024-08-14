using Domain.Core.Aggregates;
using Domain.Core.Entities;

namespace Domain.Contracts.Contracts
{
    public interface IHotelRepositorio:IRepositorioBase<Hotel>,IDisposable
    {
    }
}
