﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServicesDeskUCABWS.Data;
using ServicesDeskUCABWS.Models.DTO;
using ServicesDeskUCABWS.Persistence.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicesDeskUCABWS.BussinesLogic.Grupo_I.Gestion_de_Usuario.Services
{/*
    public class AsignacionRolServices
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AsignacionRolServices(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<RolUsuario>> GetAll()
        {
            return await _context.RolUsuarios.ToListAsync();
        }
        public async Task<RolUsuario> Create(UsuarioRolDto newUserRolDto)
        {
            var newUserRol = _mapper.Map<RolUsuario>(newUserRolDto);
            _context.RolUsuarios.Add(newUserRol);
            await _context.SaveChangesAsync();
            return newUserRol;

        }


        }*/
}
