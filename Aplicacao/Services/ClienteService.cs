using AutoMapper;
using Projeto.Application.DTO;
using Projeto.Application.Interfaces;
using Projeto.Domain.Entidades;
using Projeto.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepositorio clienteRepositorio;
        private readonly IMapper mapper;

        public ClienteService(IClienteRepositorio clienteRepositorio, IMapper mapper)
        {
            this.clienteRepositorio = clienteRepositorio;
            this.mapper = mapper;
        }

        public async Task<int> CreateAsync(ClienteDTO clienteDTO)
        {
            var cliente = mapper.Map<Cliente>(clienteDTO);
            return await clienteRepositorio.CreateAsync(cliente);
        }

        public async Task<ClienteDTO> GetByIdAsync(int? id)
        {
            var cliente = await clienteRepositorio.GetByIdAsync(id);
            return mapper.Map<ClienteDTO>(cliente);
        }

        public async Task<IEnumerable<ClienteDTO>> GetClientesAsync()
        {
            var clientes = await clienteRepositorio.GetClientesAsync();
            return mapper.Map<IEnumerable<ClienteDTO>>(clientes);
        }

        public async Task RemoveAsync(int id)
        {
            await clienteRepositorio.RemoveAsync(id);
        }

        public async Task UpdateAsync(ClienteDTO clienteDTO)
        {
            var cliente = mapper.Map<Cliente>(clienteDTO);
            await clienteRepositorio.UpdateAsync(cliente);
        }
    }
}
