using System.ComponentModel.DataAnnotations;

namespace LendThingsMVC.Models
{
    public class ThingForCreationViewModel
    {
        public ThingForCreationViewModel()
        {
        }

        public int Id { get; set; }
        [Required(ErrorMessage ="Description is Mandatory.")]
        public string Description { get; set; }
        public DateTime CreationDate { get; }
        [Required(ErrorMessage = "Category is Mandatory.")]
        public int Category { get; set; }
    }
}
