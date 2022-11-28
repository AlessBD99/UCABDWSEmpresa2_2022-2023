﻿using System;
using System.Collections.Generic;
using ServiceDeskUCAB.Models;
using ServiceDeskUCAB.Models.TipoTicketsModels;
using ServiceDeskUCAB.Models.DTO.PrioridadDTO;
using ServicesDeskUCAB.Models;

namespace ServiceDeskUCAB.ViewModel
{
    public class TicketReenviarDTOViewModel
    {
        public TicketCompletoDTO ticketPadre;
        public TicketReenviarDTO ticket;
        public List<Departament> departamentos;
        public List<PrioridadDTO> prioridades;
        public List<Tipo> tipo_tickets;
    }
}