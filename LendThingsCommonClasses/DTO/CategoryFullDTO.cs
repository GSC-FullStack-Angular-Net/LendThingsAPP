using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendThingsCommonClasses.DTO
{
    public class CategoryFullDTO
    {
        public CategoryFullDTO()
        {
        }

        [Required]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
