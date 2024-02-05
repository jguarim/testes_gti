using AutoMapper;
using Projeto.Application.DTO;
using Projeto.Domain.Entidades;
using Projeto.Domain.ObjetoValor;

namespace Projeto.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<Cliente, ClienteDTO>().ReverseMap();
            //CreateMap<EnderecoCliente, EnderecoClienteDTO>().ReverseMap();

            CreateMap<ClienteDTO, Cliente>().ReverseMap();
            CreateMap<EnderecoClienteDTO, EnderecoCliente>().ReverseMap();
        }
    }
}
