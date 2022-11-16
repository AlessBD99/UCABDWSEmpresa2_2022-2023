﻿using AutoMapper;
using ServicesDeskUCABWS.BussinesLogic.DTO.DepartamentoDTO;
using ServicesDeskUCABWS.BussinesLogic.DTO.Flujo_AprobacionDTO;
using ServicesDeskUCABWS.BussinesLogic.DTO.Tipo_CargoDTO;
using ServicesDeskUCABWS.BussinesLogic.DTO.Tipo_TicketDTO;
using ServicesDeskUCABWS.Entities;
using System.Collections.Generic;

namespace ServicesDeskUCABWS.BussinesLogic.Mappers
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            //Ejemplo
            //CreateMap<PlantillaNotificacion, PlantillaNotificacionSearchDTO>();
            //CreateMap<PlantillaNotificacionSearchDTO, PlantillaNotificacion>();

            CreateMap<Tipo_Ticket, Tipo_TicketDTOCreate>();
            CreateMap<Tipo_Ticket, Tipo_TicketDTOUpdate>();
            CreateMap<Tipo_TicketDTOUpdate, Tipo_TicketDTOCreate>();


            CreateMap<Departamento, DepartamentoSearchDTO>();
            CreateMap<DepartamentoSearchDTO, Departamento>();

            CreateMap<Departamento, DepartamentoDTO>();
            CreateMap<DepartamentoDTO, Departamento>();


            CreateMap<Flujo_Aprobacion, Flujo_AprobacionDTOSearch>();
            CreateMap<Flujo_AprobacionDTOSearch, Flujo_Aprobacion>();

            CreateMap<Tipo_Cargo, Tipo_CargoDTOSearch>();
            CreateMap<Tipo_CargoDTOSearch, Tipo_Cargo>();

            CreateMap<Tipo_Cargo, TipoCargoDTO>();
            CreateMap<TipoCargoDTO, Tipo_Cargo>();

            CreateMap<Tipo_Ticket, Tipo_TicketDTOSearch>();
            CreateMap<Tipo_TicketDTOSearch, Tipo_Ticket>();


        }
    }
}
