﻿using AutoMapper;
using ServicesDeskUCABWS.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using ServicesDeskUCABWS.Entities;
using ServicesDeskUCABWS.BussinesLogic.DTO.PrioridadDTO;

namespace ServicesDeskUCABWS.BussinesLogic.DAO.PrioridadDAO
{
    public class PrioridadDAO : IPrioridadDAO
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public PrioridadDAO(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public string crearPrioridad(PrioridadDTO prioridadDTO)
        {
            try
            {
                // var tipoEstadoCreate = tipoEstado;
                // tipoEstadoCreate.etiqueta.Clear(); 
                var prioridadEntity = _mapper.Map<Prioridad>(prioridadDTO);

                _dataContext.Prioridades.Add(prioridadEntity);
                _dataContext.SaveChanges();
                return "Prioridad creada satisfactoriamente";
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo registrar la prioridad", ex);
            }
        }

        public List<PrioridadDTO> obtenerPrioridades()
        {
            var data = _dataContext.Prioridades.AsNoTracking();
            var prioridadDTO = _mapper.Map<List<PrioridadDTO>>(data);
            return prioridadDTO.ToList();
        }

        public PrioridadDTO obtenerPrioridadPorNombre(string nombre)
        {
            try
            {
                var data = _dataContext.Prioridades.AsNoTracking().Where(p => p.nombre == nombre).Single();
                var prioridadDTO = _mapper.Map<PrioridadDTO>(data);
                return prioridadDTO;
            }
            catch (Exception ex)
            {
                throw new Exception($"No existe la Prioridad {ex}");
            }
        }

        public List<PrioridadDTO> obtenerPrioridadesPorEstado(string estado)
        {
            try
            {
                var data = _dataContext.Prioridades.AsNoTracking().Where(p => p.estado == estado);
                var prioridadDTO = _mapper.Map<List<PrioridadDTO>>(data);
                return prioridadDTO;
            }
            catch (Exception ex)
            {
                throw new Exception($"No existe la Prioridad {ex}");
            }
        }

        public string modificarPrioridad(PrioridadDTO prioridadDTO)
        {
            try
            {
                var prioridad = _dataContext.Prioridades.AsNoTracking().Where(p => p.Id == prioridadDTO.Id).Single();
                prioridad.nombre = prioridadDTO.nombre;
                prioridad.descripcion = prioridadDTO.descripcion;
                prioridad.estado = prioridadDTO.estado;
                prioridad.fecha_descripcion = prioridadDTO.fecha_descripcion;
                prioridad.fecha_ultima_edic = prioridadDTO.fecha_ultima_edic;
                _dataContext.Prioridades.Update(prioridad);
                _dataContext.SaveChanges();
                return "Prioridad modificada exitosamente";
            }
            catch (Exception ex)
            {
                throw new Exception($"No existe la Prioridad {ex}");
            }
        }
    }
}
