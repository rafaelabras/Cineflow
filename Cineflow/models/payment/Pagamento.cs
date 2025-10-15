using Cineflow.models.cinema;
using Cineflow.models.enums;
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
        private string? Id_reserva { get; set; }
        private Reserva? reserva { get; set; }
        [Required]
        private MetodoPagamento? metodo { get; set; }
        [Required]
        private decimal? valor { get; set; }
        [Required]
        private bool? pago { get; set; }
        [Required]
        private string? transacao_gateway { get; set; }
        [Required]
        private DateTime? data_pagamento { get; set; }

    }
}
