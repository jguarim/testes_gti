using System.ComponentModel.DataAnnotations;

namespace Projeto.Application.DTO
{
    public class EnderecoClienteDTO
    {
        public int EnderecoId { get; set; }

        [Required(ErrorMessage = "Cep campo obrigatório.")]
        [MaxLength(8, ErrorMessage = "Máximo 8 caracteres.")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "Logradouro campo obrigatório.")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Numero campo obrigatório.")]
        [MaxLength(10, ErrorMessage = "Máximo 10 caracteres.")]
        public string Numero { get; set; }

        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres.")]
        public string Complemento { get; set; }

        [Required(ErrorMessage = "Bairro campo obrigatório.")]
        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres.")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Cidade campo obrigatório.")]
        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres.")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "UF campo obrigatório.")]
        [MaxLength(2, ErrorMessage = "Máximo 2 caracteres.")]
        public string UF { get; set; }
    }
}
