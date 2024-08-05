﻿using RealStateApp.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace RealStateApp.Core.Application.DataTransferObjects.AmenityDTOs
{
    public class AmenityForCreationDTO
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Debe colocar un nombre")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debe colocar la descripción")]
        [DataType(DataType.Text)]
        public string Description { get; set; }
        public ICollection<Property>? Properties { get; set; }
    }
}