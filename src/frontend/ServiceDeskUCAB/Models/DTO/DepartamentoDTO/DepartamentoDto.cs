﻿using System;
using System.Text.Json.Serialization;

namespace ServiceDeskUCAB.Models.DTO.DepartamentoDTO
{
    public class DepartamentoDto
    {
        public Guid id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public DateTime fecha_creacion { get; set; } = DateTime.Now.Date;
        public DateTime? fecha_ultima_edicion { get; set; }
        public DateTime? fecha_eliminacion { get; set; }
    }

    public class DepartamentoDto_Update
    {
        public Guid id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime? fecha_ultima_edicion { get; set; } = DateTime.Now.Date;
        public DateTime? fecha_eliminacion { get; set; }

        [JsonIgnore]
        public Guid? id_grupo { get; set; } = null;
    }
}