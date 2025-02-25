﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServicesDeskUCABWS.BussinesLogic.DTO.Etiqueta;
using ServicesDeskUCABWS.BussinesLogic.Exceptions;
using ServicesDeskUCABWS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesDeskUCABWS.BussinesLogic.DAO.EtiquetaDAO
{
    public class EtiquetaService : IEtiqueta
    {
        private readonly IDataContext _EtiquetaContext;
        private readonly IMapper _mapper;

        public EtiquetaService(IDataContext etiquetaContext, IMapper mapper)
        {
            _EtiquetaContext = etiquetaContext;
            _mapper = mapper;
        }

        //GET: Servicio para consultar todas las etiquetas
        public List<EtiquetaDTO> ConsultaEtiquetas()
        {
            try {
                var data = _EtiquetaContext.Etiquetas.AsNoTracking().ToList();
                var etiDTO = _mapper.Map<List<EtiquetaDTO>>(data);
                if (etiDTO.Count() == 0)
                    throw new ExceptionsControl("No existen etiquetas registradas");
                return etiDTO;
            }
            catch (ExceptionsControl ex)
            {
                throw new ExceptionsControl("No existen etiquetas registradas", ex);
            }
            catch (Exception ex)
            {
                throw new ExceptionsControl("Hubo un problema al consultar las etiquetas", ex);

            }
        }

        //GET: Servicio para consultar una plantilla notificacion en especifico
        public EtiquetaDTO ConsultarEtiquetaGUID(Guid id)
        {
            try
            {
                var data = _EtiquetaContext.Etiquetas.AsNoTracking().Where(p => p.Id == id).Single();
                var etiDTO = _mapper.Map<EtiquetaDTO>(data);
                return etiDTO;
            }
            catch (Exception ex)
            {
                throw new ExceptionsControl("No existe la etiqueta con ese ID", ex);
            }

        }
    }
}
