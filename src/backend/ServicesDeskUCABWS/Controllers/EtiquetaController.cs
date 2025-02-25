﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using ServicesDeskUCABWS.BussinesLogic.DAO.EtiquetaDAO;
using ServicesDeskUCABWS.BussinesLogic.Response;
using ServicesDeskUCABWS.BussinesLogic.DTO.Etiqueta;
using ServicesDeskUCABWS.BussinesLogic.Exceptions;
using System.Threading.Tasks;

namespace ServicesDeskUCABWS.Controllers
{
        [Route("Etiqueta")]
        [ApiController]
        public class EtiquetaController : ControllerBase
        {
            private readonly IEtiqueta _etiqueta;

            public EtiquetaController(IEtiqueta etiqueta)
            {
                _etiqueta = etiqueta;
            }

            //GET: Controlador para consultar todas las etiquetas
            [HttpGet]
            [Route("Consulta/")]
            public ApplicationResponse<List<EtiquetaDTO>> ConsultaEtiquetasCtrl()
            {
                var response = new ApplicationResponse<List<EtiquetaDTO>>();

                try
                {
                    response.Data = _etiqueta.ConsultaEtiquetas();
                }
                catch (ExceptionsControl ex)
                {
                    response.Success = false;
                    response.Message = ex.Mensaje;
                    response.Exception = ex.Excepcion.ToString();
                }
                return response;
            }

            //GET: Controlador para consultar una etiqueta en especifico
            [HttpGet]
            [Route("Consulta/{id}")]
            public ApplicationResponse<EtiquetaDTO> GetEtiquetaByGuidCtrl(Guid id)
        {
                var response = new ApplicationResponse<EtiquetaDTO>();

                try
                {
                    response.Data = _etiqueta.ConsultarEtiquetaGUID(id);
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
