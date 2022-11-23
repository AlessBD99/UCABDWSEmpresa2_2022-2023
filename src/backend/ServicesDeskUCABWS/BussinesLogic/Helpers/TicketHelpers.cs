﻿using ServicesDeskUCABWS.Entities;
using System.Collections.Generic;
using System;
using AutoMapper;
using ServicesDeskUCABWS.Data;
using ServicesDeskUCABWS.BussinesLogic.DTO.TicketsDTO;
using System.Linq;
using ServicesDeskUCABWS.BussinesLogic.DTO.TicketDTO;
using ServicesDeskUCABWS.BussinesLogic.Excepciones;
using ServicesDeskUCABWS.BussinesLogic.Validaciones;
using System.Net.Sockets;
using ServicesDeskUCABWS.BussinesLogic.ApplicationResponse;

namespace ServicesDeskUCABWS.BussinesLogic.Helpers
{
    public class TicketHelpers
    {

        private readonly IDataContext _dataContext;
        private readonly IMapper _mapper;

        public TicketHelpers(IDataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public TicketDTO crearNuevoTicket(TicketNuevoDTO solicitudTicket)
        {
            TicketDTO nuevoTicket = _mapper.Map<TicketDTO>(solicitudTicket);
            nuevoTicket.Id = new Guid();
            nuevoTicket.fecha_creacion = DateTime.Today;
            nuevoTicket.fecha_eliminacion = DateTime.MinValue;
            nuevoTicket.Emisor = _dataContext.Empleados.Where(empleado => empleado.Id == solicitudTicket.empleado_id).FirstOrDefault();
            nuevoTicket.Departamento_Destino = _dataContext.Departamentos.Where(departamento => departamento.Id == solicitudTicket.departamentoDestino_Id).FirstOrDefault();
            nuevoTicket.Estado = _dataContext.Estados.Where(x => x.Id == new Guid("D198AFEE-6141-1E4F-9A78-0D6222F1B8DB")).FirstOrDefault();
            nuevoTicket.Prioridad = _dataContext.Prioridades.Where(prioridad => prioridad.Id == solicitudTicket.prioridad_id).FirstOrDefault();
            nuevoTicket.Tipo_Ticket = _dataContext.Tipos_Tickets.Where(tipoTicket => tipoTicket.Id == solicitudTicket.tipoTicket_id).FirstOrDefault();
            //nuevoTicket.Ticket_Padre = null;
            inicializarBitacora(nuevoTicket);
            _dataContext.Tickets.AddAsync(_mapper.Map<Ticket>(nuevoTicket));
            _dataContext.DbContext.SaveChangesAsync();
            return nuevoTicket;
        }
        public void inicializarBitacora(TicketDTO nuevoTicketDTO)
        {
            nuevoTicketDTO.Bitacora_Tickets = new HashSet<Bitacora_Ticket>();
            crearNuevaBitacora(nuevoTicketDTO);
        }

        public Bitacora_Ticket crearNuevaBitacora(TicketDTO ticket)
        {
            Bitacora_Ticket nuevaBitacora = new Bitacora_Ticket()
            {
                Id = Guid.NewGuid(),
                Estado = ticket.Estado,
                Ticket = _mapper.Map<Ticket>(ticket),
                Fecha_Inicio = new DateTime(),
                Fecha_Fin = DateTime.MinValue
            };
            ticket.Bitacora_Tickets.Add(nuevaBitacora);
            _dataContext.Bitacora_Tickets.Add(ticket.Bitacora_Tickets.First());
            _dataContext.DbContext.SaveChangesAsync();
            return nuevaBitacora;
        }

        public void modificarEstadoTicket(Guid ticketId, Guid estadoId)
        {
            TicketValidaciones ticketValidaciones = new TicketValidaciones(_dataContext);
            ticketValidaciones.validarTicket(ticketId);
            TicketDTO ticket = _mapper.Map<TicketDTO>(_dataContext.Tickets.Where(tickets => tickets.Id == ticketId).Single());
            Estado nuevoEstado = _dataContext.Estados.Where(estados => estados.Id == estadoId).Single();
            ticket.Estado = nuevoEstado;
            ticket.Bitacora_Tickets.Add(crearNuevaBitacora(ticket));
            _dataContext.DbContext.Update(_mapper.Map<Ticket>(ticket));
            _dataContext.DbContext.SaveChangesAsync();
        }
        public List<TicketBitacorasDTO> obtenerBitacoras(Guid ticketId)
        {
            TicketValidaciones ticketValidaciones = new TicketValidaciones(_dataContext);
            ticketValidaciones.validarTicket(ticketId);
            TicketDTO ticket = _mapper.Map<TicketDTO>(_dataContext.Tickets.Where(ticket => ticket.Id == ticketId).Single());
            List<TicketBitacorasDTO> bitacoras = new List<TicketBitacorasDTO>();
            ticket.Bitacora_Tickets.ToList().ForEach(delegate (Bitacora_Ticket bitacora)
            {
                bitacoras.Add(new TicketBitacorasDTO
                {
                    Id = bitacora.Id,
                    estado_nombre = bitacora.Estado.nombre,
                    fecha_inicio = bitacora.Fecha_Inicio,
                    fecha_fin = bitacora.Fecha_Fin
                });
            });
            return bitacoras;
        }

        public void inicializarFamiliaTicket(TicketDTO nuevoTicket)
        {   //PARA LOS TICKETS HERMANOS
            nuevoTicket.Familia_Ticket = new Familia_Ticket()
            {
                Id = Guid.NewGuid(),
                Lista_Ticket = new List<Ticket>()
            };
        }
        public TicketInfoCompletaDTO rellenarTicketInfoCompleta(Guid id)
        {
            TicketDTO ticket = _mapper.Map<TicketDTO>(_dataContext.Tickets.Where(ticket => ticket.Id == id));
            Guid idPadre;
            if (ticket.Ticket_Padre.Id.Equals(Guid.Empty))
                idPadre = Guid.Empty;
            else
                idPadre = ticket.Ticket_Padre.Id;
            return new TicketInfoCompletaDTO
            {
                ticket_id = id,
                ticketPadre_id = idPadre,
                fecha_creacion = ticket.fecha_creacion,
                fecha_eliminacion = ticket.fecha_eliminacion,
                titulo = ticket.titulo,
                descripcion = ticket.descripcion,
                estado_nombre = ticket.Estado.nombre,
                tipoTicket_nombre = ticket.Tipo_Ticket.nombre,
                departamentoDestino_nombre = ticket.Departamento_Destino.nombre,
                prioridad_nombre = ticket.Prioridad.nombre,
                empleado_correo = ticket.Emisor.correo
            };
        }
        public List<TicketInfoBasicaDTO> rellenarTicketInfoBasica(Guid idDepartamento, string opcion)
        {
            List<TicketDTO> tickets;
            if (opcion == "Todos")
                tickets = _mapper.Map<List<TicketDTO>>(_dataContext.Tickets.Where(ticket => ticket.Departamento_Destino.Id == idDepartamento).ToList());
            else if (opcion == "Abiertos")
                tickets = _mapper.Map<List<TicketDTO>>(_dataContext.Tickets.Where(ticket => ticket.Departamento_Destino.Id == idDepartamento && ticket.fecha_eliminacion.Equals(DateTime.MinValue)).ToList());
            else if (opcion == "Cerrados")
                tickets = _mapper.Map<List<TicketDTO>>(_dataContext.Tickets.Where(ticket => ticket.Departamento_Destino.Id == idDepartamento && !ticket.fecha_eliminacion.Equals(DateTime.MinValue)).ToList());
            else
                throw new TicketException("Lista de tickets no encontrada debido a que la opción de búsqueda no es válido");
            if (tickets.Count == 0)
                throw new TicketException("No existen tickets que satisfagan el tipo de búsqueda");
            List<TicketInfoBasicaDTO> respuesta = new List<TicketInfoBasicaDTO>();
            tickets.ForEach(delegate (TicketDTO ticket)
            {
                respuesta.Add(_mapper.Map<TicketInfoBasicaDTO>(ticket));
            });
            return respuesta;
        }
        public void mergeTickets(Guid ticketPrincipalId, List<Guid> ticketsSecundariosId)
        {
            TicketValidaciones ticketValidaciones = new TicketValidaciones(_dataContext);
            ticketValidaciones.validarTicket(ticketPrincipalId);
            ticketsSecundariosId.ForEach(delegate (Guid id)
            {
                ticketValidaciones.validarTicket(id);
                TicketDTO ticket = _mapper.Map<TicketDTO>(_dataContext.Tickets.Where(ticket => ticket.Id == id).Single());
                Estado estado = _dataContext.Estados.Where(estado => estado.nombre == "").Single();
                modificarEstadoTicket(ticket.Id, estado.Id);
                ticket.fecha_eliminacion = new DateTime();
            });
            Estado estado = _dataContext.Estados.Where(estado => estado.nombre == "").Single();
            modificarEstadoTicket(ticketPrincipalId, estado.Id);
        }

        public List<TicketInfoBasicaDTO> obtenerFamiliaTickets(Guid id)
        {
            TicketValidaciones ticketValidaciones = new TicketValidaciones(_dataContext);
            ticketValidaciones.validarTicket(id);
            List<TicketInfoBasicaDTO> listaTickets = new List<TicketInfoBasicaDTO>();
            TicketDTO ticket = _mapper.Map<TicketDTO>(_dataContext.Tickets.Where(ticket => ticket.Id == id).Single());
            
            return listaTickets;
        }
    }
}
