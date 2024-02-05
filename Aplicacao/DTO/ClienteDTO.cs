using System;
using System.ComponentModel.DataAnnotations;

namespace Projeto.Application.DTO
{
    public class ClienteDTO
    {
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "Cpf campo é obrigatório.")]
        [MaxLength(11, ErrorMessage = "Cpf inválido.")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Nome campo é obrigatório.")]
        [MinLength(3, ErrorMessage = "Mínimo 3 caracteres.")]
        [MaxLength(100)]
        public string Nome { get; set; }

        public string Rg { get; set; }

        [Display(Name = "Data Expedição")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataExpedicao { get; set; }

        [Display(Name = "Orgão Expeditor")]
        public string OrgaoExpedicao { get; set; }

        public string UF { get; set; }

        [Display(Name = "Data Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Data nascimento campo é obrigatório.")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "Sexo campo é obrigatório.")]
        [MinLength(1, ErrorMessage = "Digite M - Masculino / F - Feminino")]
        public string Sexo { get; set; }

        [Display(Name = "Estado Civil", AutoGenerateFilter = false)]
        [MinLength(3, ErrorMessage = "Estado civil campo é obrigatório.")]
        public string EstadoCivil { get; set; }

        public EnderecoClienteDTO Endereco { get; set; }
    }
}
