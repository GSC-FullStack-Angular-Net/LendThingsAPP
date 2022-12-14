using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendThingsCommonClasses.DTO
{
    public class ThingFullDTO
    {
        public ThingFullDTO()
        {
        }

        public int Id { get; set; }
        [Required]
        public string Description { get; set; }

        public DateTime CreationDate { get; set; }
        [Required]
        public CategoryFullDTO Category { get; set; }
    }
}
