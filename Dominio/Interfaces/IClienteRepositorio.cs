using Projeto.Domain.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto.Domain.Interfaces
{
    public interface IClienteRepositorio
    {
        Task<IEnumerable<Cliente>> GetClientesAsync();

        Task<Cliente> GetByIdAsync(int? id);

        Task<int> CreateAsync(Cliente cliente);

        Task UpdateAsync(Cliente cliente);

        Task RemoveAsync(int id);
    }
}
