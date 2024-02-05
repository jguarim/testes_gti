using Projeto.Domain.ObjetoValor;
using Projeto.Domain.Validation;
using System;

namespace Projeto.Domain.Entidades
{
    public class Cliente
    {
        public int ClienteId { get; private set; }

        public string Cpf { get; private set; }

        public string Nome { get; private set; }

        public string Rg { get; private set; }

        public DateTime DataExpedicao { get; private set; }

        public string OrgaoExpedicao { get; private set; }

        public string UF { get; private set; }

        public DateTime DataNascimento { get; private set; }

        public string Sexo { get; private set; }

        public string EstadoCivil { get; private set; }

        public int EnderecoId { get; set; }

        public EnderecoCliente Endereco { get; set; }
        private Cliente()
        {
        }
        public Cliente(int clienteId, string cpf, string nome, string rg, DateTime dataExpedicao, string orgaoExpedicao, string uF, DateTime dataNascimento, string sexo, string estadoCivil, int enderecoId, EnderecoCliente endereco)
        {
            DomainValidation.When(clienteId < 0, "Invalid Id value.");
            ClienteId = clienteId;
            Validation(cpf, nome, dataNascimento, sexo, estadoCivil);

            ClienteId = clienteId;
            Cpf = cpf;
            Nome = nome;
            Rg = rg;
            DataExpedicao = dataExpedicao;
            OrgaoExpedicao = orgaoExpedicao;
            UF = uF;
            DataNascimento = dataNascimento;
            Sexo = sexo;
            EstadoCivil = estadoCivil;
            EnderecoId = enderecoId;
            Endereco = endereco;
        }

        public Cliente(string cpf, string nome, string rg, DateTime dataExpedicao, string orgaoExpedicao, string uF, DateTime dataNascimento, string sexo, string estadoCivil, int enderecoId, EnderecoCliente endereco)
        {
            Validation(cpf, nome, dataNascimento, sexo, estadoCivil);
            Cpf = cpf;
            Nome = nome;
            Rg = rg;
            DataExpedicao = dataExpedicao;
            OrgaoExpedicao = orgaoExpedicao;
            UF = uF;
            DataNascimento = dataNascimento;
            Sexo = sexo;
            EstadoCivil = estadoCivil;
            EnderecoId = enderecoId;
            Endereco = endereco;
        }

        public void Update(string cpf, string nome, string rg, DateTime dataExpedicao, string orgaoExpedicao, string uF, DateTime dataNascimento, string sexo, string estadoCivil, int enderecoId, EnderecoCliente endereco)
        {
            Validation(cpf, nome, dataNascimento, sexo, estadoCivil);
            Cpf = cpf;
            Nome = nome;
            Rg = rg;
            DataExpedicao = dataExpedicao;
            OrgaoExpedicao = orgaoExpedicao;
            UF = uF;
            DataNascimento = dataNascimento;
            Sexo = sexo;
            EstadoCivil = estadoCivil;
            EnderecoId = enderecoId;
            Endereco = endereco;
        }

        private void Validation(string cpf, string nome, DateTime dataNascimento, string sexo, string estadoCivil)
        {
            DomainValidation.When(string.IsNullOrEmpty(cpf), "Cpf é obrigatório.");
            DomainValidation.When(string.IsNullOrEmpty(nome), "Nome é obrigatório.");
            DomainValidation.When(string.IsNullOrEmpty(sexo), "Sexo é obrigatório.");
            DomainValidation.When(string.IsNullOrEmpty(estadoCivil), "Estado civil é obrigatório.");

            if (!Validacao.Validacao.ValidaCpf.IsCpf(cpf))
            {
                throw new Exception("Cpf inválido.");
            }

            if (!Validacao.Validacao.ValidarData(dataNascimento.ToShortDateString()))
            {
                throw new Exception("Data inválida.");
            }

            Cpf = cpf;
            Nome = nome;
            DataNascimento = dataNascimento;
            Sexo = sexo;
            EstadoCivil = estadoCivil;
        }
    }
}
