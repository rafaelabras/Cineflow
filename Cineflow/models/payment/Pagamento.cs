using Cineflow.enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cineflow.models.payment
{
    public class Pagamento
    {
        [Required]
        [Key]
        private Guid ID { get; set; } = new Guid();
        [Required]
        private MetodoPagamento? metodo { get; set; }
        [Required]
        private decimal? valor { get; set; }
        [Required]
        private bool? status { get; set; }
        [Required]
        private string? transacao_gateway { get; set; }
        [Required]
        private DateTime? data_pagamento { get; set; }

    }
}
