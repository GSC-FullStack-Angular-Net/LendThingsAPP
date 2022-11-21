using LendThingsCommonClasses.DTO;
using System.ComponentModel.DataAnnotations;

namespace LendThingsMVC.Models
{
    public class ThingViewModel
    {
        public ThingViewModel() { }

        public int Id { get; set; }
        [Required]
        public string Description { get; set; }

        public DateTime CreationDate { get; }
        [Required]
        public CategoryViewModel Category { get; set; }
    }
}
