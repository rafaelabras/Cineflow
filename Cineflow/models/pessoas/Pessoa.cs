﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Cineflow.models.pessoas
{
    public class Pessoa
    {


        [Key]
        [Required]
        public Guid ID { get; set; }
        [Required]
        public string? CPF { get; set; }  // o id é o cpf
        [Required]
        [Length(10, 40)]
        public string? name { get; set; }
        [Required]
        [EmailAddress]
        public string? email { get; set; }
        [Required]
        public string? senhaHash { get; set; }
        [Required]
        public DateTime? data_nascimento { get; set; }
        [Required]
        [Phone]
        public string? telefone { get; set; }


    }
}
