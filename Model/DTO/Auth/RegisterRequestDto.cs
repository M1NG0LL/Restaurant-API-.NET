﻿using System.ComponentModel.DataAnnotations;

namespace Restaurant_API.Model.DTO.Auth
{
    public class RegisterRequestDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
