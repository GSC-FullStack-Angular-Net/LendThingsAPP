using LendThingsCommonClasses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendThingsCommonClasses.DTO
{
    public class ThingBaseDTO
    {
        public ThingBaseDTO()
        {
        }

        public int Id {  get; set; }
        public string Description { get; set; }
        public int Category { get; set; }

    }
}
