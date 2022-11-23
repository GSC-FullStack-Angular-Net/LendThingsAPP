using System.ComponentModel.DataAnnotations;

namespace LendThingsMVC.Models
{
    public class CategoryViewModel
    {
        public CategoryViewModel()
        {
        }

        public int Id { get; set; }
        [Required]
        [Display(Name = "Category")]
        public string Description { get; set; }
    }
}
