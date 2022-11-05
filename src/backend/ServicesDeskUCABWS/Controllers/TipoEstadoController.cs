﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.BussinesLogic.DTO.EtiquetaTipoEstado;
using ServicesDeskUCABWS.BussinesLogic.DTO.TipoEstado;
using ServicesDeskUCABWS.BussinessLogic.DAO.PlantillaNotificacioneDAO;
using ServicesDeskUCABWS.BussinessLogic.DAO.TipoEstadoDAO;
using ServicesDeskUCABWS.BussinessLogic.DTO.Etiqueta;
using ServicesDeskUCABWS.BussinessLogic.DTO.Plantilla;
using ServicesDeskUCABWS.BussinessLogic.DTO.TipoEstado;
using ServicesDeskUCABWS.BussinessLogic.Exceptions;
using ServicesDeskUCABWS.Entities;
using ServicesDeskUCABWS.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicesDeskUCABWS.Controllers
{
    [Route("TipoEstado")]
    [ApiController]
    public class TipoEstadoController:ControllerBase
    {
        private readonly ITipoEstado _tipoEstado;
        

        public TipoEstadoController(ITipoEstado tipoEstadoContext)
        {
            _tipoEstado = tipoEstadoContext;
            
        }

        //GET: Controlador para consultar todas los tipos estados
        [HttpGet]
        [Route("Consulta/")]
        public async Task<ApplicationResponse<List<TipoEstadoDTO>>> ConsultaTipoEstadosCtrl()
        {
            var response = new ApplicationResponse<List<TipoEstadoDTO>>();

            try
            {
                response.Data = await _tipoEstado.ConsultaTipoEstados();
            }
            catch (ExceptionsControl ex)
            {
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }

        //GET: Controlador para consultar un tipo estado en especifico
        [HttpGet]
        [Route("Consulta/{id}")]
        public async Task<ActionResult<ApplicationResponse<TipoEstadoDTO>>> GetByGuidCtrl(Guid id)
        {
            var response = new ApplicationResponse<TipoEstadoDTO>();

            try
            {
                response.Data = await _tipoEstado.ConsultarTipoEstadoGUID(id);
            }
            catch (ExceptionsControl ex)
            {
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;

        }

        //GET: Controlador para consultar una tipo estado por un título específico
        [HttpGet]
        [Route("Consulta/Titulo/{titulo}")]
        public async Task<ApplicationResponse<TipoEstadoDTO>> GetByTituloCtrl(string titulo)
        {
            var response = new ApplicationResponse<TipoEstadoDTO>();
            try
            {
                response.Data = await _tipoEstado.ConsultarTipoEstadoTitulo(titulo);
            }
            catch (ExceptionsControl ex)
            {
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }

        //POST: Controlador para crear tipo estado **
        [HttpPost]
        [Route("Registro/")]
        public async Task<ApplicationResponse<String>> CrearTipoEstadoCtrl( TipoEstadoCreateDTO tipoEstadoDTO)
        {
            var response = new ApplicationResponse<String>();
            try
            {
                var resultSevice = await _tipoEstado.RegistroTipoEstado(tipoEstadoDTO);
                response.Data = resultSevice.ToString();
            }
            catch (ExceptionsControl ex)
            {
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }

        //PUT: Controlador para modificar tipo estado
        [HttpPut]
        [Route("Actualizar/{id}")]
        public async Task<ApplicationResponse<String>> ActualizarTipoEstadoCtrl([FromBody]TipoEstadoUpdateDTO tipoEstadoDTO,[FromRoute] Guid id)
        {
            var response = new ApplicationResponse<String>();
            try
            {
                var resultSevice = await _tipoEstado.ActualizarTipoEstado(tipoEstadoDTO, id);
                response.Data = resultSevice.ToString();
            }
            catch (ExceptionsControl ex)
            {
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }

        //DELETE: Controlador para eliminar tipo estado
        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<ApplicationResponse<String>> EliminarTipoEstadoCtrl(Guid id)
        {
            var response = new ApplicationResponse<String>();
            try
            {
                var resultSevice = await _tipoEstado.EliminarTipoEstado(id);
                response.Data = resultSevice.ToString();
                      
            }
            catch (ExceptionsControl ex)
            {
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion.ToString();
            } 
            return response;
        }

    }
}
