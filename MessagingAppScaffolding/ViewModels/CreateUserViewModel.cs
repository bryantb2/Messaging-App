﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MessagingApp.ViewModels
{
    public class CreateUserViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [UIHint("email")]
        public string Email { get; set; }
        [Required]
        [UIHint("password")]
        public string Password { get; set; }
        [Required]
        [UIHint("password")]
        public string ConfirmPassword { get; set; }
    }
}
