﻿using System.ComponentModel.DataAnnotations;

namespace EinTech.Api.Contracts.Models
{
    public class CreatePersonModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int GroupId { get; set; }
    }
}
