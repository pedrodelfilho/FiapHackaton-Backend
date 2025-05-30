﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Request
{
    public class DesativarUsuarioRequest
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Email { get; set; }
    }
}
