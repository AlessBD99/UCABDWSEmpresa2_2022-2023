﻿using ServicesDeskUCABWS.BussinesLogic.Grupo_H.DTO;
using AutoMapper;
using System;
using ServicesDeskUCABWS.Persistence.Entities;

namespace ServicesDeskUCABWS.BussinesLogic.Grupo_H.Mappers
{
	public class GrupoMapper:Profile
	{

		public static GrupoDto MapperEntityToDto(Grupo grupo)
		{
			return new GrupoDto
			{
				Id = Guid.NewGuid(),
				nombre = grupo.nombre,
				descripcion = grupo.descripcion,
				fecha_creacion = DateTime.Now.Date
			};
		}

		public static GrupoDto MapperEntityToDtoDefault(Grupo grupo)
		{
			return new GrupoDto()
			{
				Id = grupo.Id,
				nombre = grupo.nombre,
				descripcion = grupo.descripcion,
				fecha_creacion = DateTime.Now.Date
			};

		}

		public static Grupo MapperDTOToEntity(GrupoDto grupo)
		{
			return new Grupo
			{
				Id = Guid.NewGuid(),
				nombre = grupo.nombre,
				descripcion = grupo.descripcion,
				fecha_creacion = grupo.fecha_creacion
			};
		}

		public static Grupo MapperDTOToEntityModificar(GrupoDto_Update grupo)
		{
			return new Grupo
			{
				Id = grupo.Id,
				nombre = grupo.nombre,
				descripcion = grupo.descripcion,
				fecha_creacion = grupo.fecha_creacion,
				fecha_ultima_edicion = grupo.fecha_ultima_edicion
			};
		}
	}
}
