﻿using Microsoft.VisualBasic;

namespace ServicesDeskUCABWS.BussinesLogic.DTO.TicketDTO
{
    public class TicketInfoBasicaDTO
    {
        public string titulo { get; set; } = string.Empty;
        public string empleado_correo { get; set; }
        public string prioridad_nombre { get; set; }
        public DateAndTime fecha_creacion { get; set; }
        public string tipoTicket_nombre { get; set; }
        public string departamentoDestino_nombre { get; set; }
    }
}
