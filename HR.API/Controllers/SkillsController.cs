using HR.API.DTOs;
using HR.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillsService skillService;

        public SkillsController(ISkillsService skService)
        {
            skillService = skService;
        }

        [HttpGet]
        public async Task<IEnumerable<Skill>> GetSkills()
        {
            var skills = await skillService.GetSkillsAsync();
            return skills;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Skill>> GetSkillAsync(Guid id)
        {
            var skill = await skillService.GetSkillAsync(id);
            if (skill == null)
            {

                return NotFound();
            }
            return Ok(skill);



        }

        [HttpPost]
        public async Task<ActionResult<SkillDTO>> PostSkill(SkillDTO skillDto)
        {

            
            Skill skill = new()
            {
                Name = skillDto.Name
            };

            await skillService.CreateSkillAsync(skill);

            return Ok(skillDto);

            



        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkillAsync(Guid id)
        {


            var skill = await skillService.GetSkillAsync(id);
            if (skill is null)
            {

                return NotFound();
            }
             await skillService.DeleteSkillAsync(id);
            return NoContent();

           
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkillAsync(Guid id,SkillDTO skill)
        {

            var existingSkill = await skillService.GetSkillAsync(id);
            if(existingSkill is null)
            {
                return NotFound();
            }

            Skill s = new()
            {   Id = existingSkill.Id,
                Name = skill.Name
            };

            await skillService.UpdateSkillAsync(s);

            return NoContent();

            
        }
    }


}

