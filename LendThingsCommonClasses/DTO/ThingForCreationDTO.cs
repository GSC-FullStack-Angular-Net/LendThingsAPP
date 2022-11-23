using LendThingsCommonClasses.Models;
using System.ComponentModel.DataAnnotations;

namespace LendThingsCommonClasses.DTO
{
    public class ThingForCreationDTO
    {
        public ThingForCreationDTO()
        {
        }

        [Required]
        public string Description { get; set; }
        [Required]
        public int Category { get; set; }
    }
}