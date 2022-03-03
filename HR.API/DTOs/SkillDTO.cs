using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR.API.DTOs
{
    public class SkillDTO
    {

        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

    }
}
