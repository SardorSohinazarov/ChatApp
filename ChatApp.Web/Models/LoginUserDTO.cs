﻿using System.ComponentModel.DataAnnotations;

namespace ChatApp.Web.Models
{
    public class LoginUserDTO
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}