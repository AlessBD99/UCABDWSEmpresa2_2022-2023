﻿using System.Collections.Generic;
using System;
using ServiceDeskUCAB.Models.DTO.DepartamentoDTO;
using ServiceDeskUCAB.Models.DTO.Flujo_AprobacionDTO;

namespace ServiceDeskUCAB.Models.DTO.Tipo_TicketDTO
{
    public class Tipo_TicketDTOSearch
    {
        public Guid Id { get; set; }

        public string nombre { get; set; } = string.Empty;

        public string descripcion { get; set; } = string.Empty;

        public string tipo { get; set; }

        public int? Minimo_Aprobado { get; set; }

        public int? Maximo_Rechazado { get; set; }

        public List<Flujo_AprobacionDTOSearch> Flujo_Aprobacion { get; set; }
        public List<DepartamentoSearchDTO> Departamento { get; set; }
    }
}
