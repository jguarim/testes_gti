using Projeto.Domain.Validation;

namespace Projeto.Domain.ObjetoValor
{
    public class EnderecoCliente
    {
        public int EnderecoId { get; set; }

        public string Cep { get; private set; }

        public string Logradouro { get; private set; }

        public string Numero { get; private set; }

        public string Complemento { get; private set; }

        public string Bairro { get; private set; }

        public string Cidade { get; private set; }

        public string UF { get; private set; }

        private EnderecoCliente()
        {
        }

        public EnderecoCliente(int enderecoId, string cep, string logradouro, string numero, string complemento, string bairro, string cidade, string uF)
        {
            EnderecoId = enderecoId;
            Cep = cep;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            UF = uF;
        }

        public void Update(string cep, string logradouro, string numero, string bairro, string cidade, string uF)
        {
            ValidationDomain(cep, logradouro, numero, bairro, cidade, uF);
        }

        private void ValidationDomain(string cep, string Logradouro, string numero, string bairro, string cidade, string uF)
        {
            DomainValidation.When(string.IsNullOrEmpty(cep), "Cep is required");
            DomainValidation.When(string.IsNullOrEmpty(Logradouro), "Logradouro is required");
            DomainValidation.When(string.IsNullOrEmpty(numero), "Numero is required");
            DomainValidation.When(string.IsNullOrEmpty(bairro), "Bairro is required");
            DomainValidation.When(string.IsNullOrEmpty(cidade), "Cidade is required");
            DomainValidation.When(string.IsNullOrEmpty(uF), "Uf is required");

            Cep = cep;
            this.Logradouro = Logradouro;
            Numero = numero;
            Bairro = bairro;
            Cidade = cidade;
            UF = uF;
        }
    }
}
