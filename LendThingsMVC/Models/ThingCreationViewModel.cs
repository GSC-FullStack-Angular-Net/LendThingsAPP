using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LendThingsMVC.Models
{
    public class ThingCreationViewModel
    {
        public ThingCreationViewModel()
        {
        }

        [Required(ErrorMessage ="Description is Mandatory.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Category is Mandatory.")]
        public int Category { get; set; }

    }
}
