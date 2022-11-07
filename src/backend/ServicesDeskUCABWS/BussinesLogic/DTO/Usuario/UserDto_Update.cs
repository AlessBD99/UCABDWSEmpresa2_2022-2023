﻿using System.ComponentModel.DataAnnotations;
using System;

namespace ServicesDeskUCABWS.BussinesLogic.DTO.Usuario
{
    public class UserDto_Update
    {
        [Required]
        public Guid id { get; set; }
        [Required]
        public int cedula { get; set; }
        [Required]
        [MaxLength(50)]
        public string primer_nombre { get; set; } = string.Empty;

        [MaxLength(50)]
        public string segundo_nombre { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string primer_apellido { get; set; } = string.Empty;
        [MaxLength(50)]
        public string segundo_apellido { get; set; } = string.Empty;
        [Required]
        public DateTime fecha_nacimiento { get; set; }
    }
}
