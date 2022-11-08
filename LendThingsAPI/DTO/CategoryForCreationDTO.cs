using System.ComponentModel.DataAnnotations;

namespace LendThingsAPI.DTO
{
    public class CategoryForCreationDTO
    {
        public CategoryForCreationDTO()
        {
        }
        [Required(ErrorMessage = "Description is mandatory")]
        public string Description { get; set; }
    }
}