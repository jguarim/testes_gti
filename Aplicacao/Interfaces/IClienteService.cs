using Projeto.Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto.Application.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDTO>> GetClientesAsync();

        Task<ClienteDTO> GetByIdAsync(int? id);

        Task<int> CreateAsync(ClienteDTO clienteDTO);

        Task UpdateAsync(ClienteDTO clienteDTO);

        Task RemoveAsync(int id);
    }
}
