using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HR.Data;
using Models;
using HR.Service;
using HR.API.DTOs;

namespace HR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidatesService candidatesService;
        private readonly ISkillsService skillsService;

        public CandidatesController(ICandidatesService canService,ISkillsService sklService)
        {
            candidatesService = canService;
            skillsService = sklService;
        }

        

        // GET: api/Candidates
        [HttpGet]
        public async Task<IEnumerable<Candidate>> GetCandidatesAsync()
        {
            var candidates = await candidatesService.GetCandidatesAsync();
            return candidates;
        }

        // GET: api/Candidates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Candidate>> GetCandidateAsync(Guid id)
        {
            var candidate = await candidatesService.GetCandidateAsync(id);

            if (candidate == null)
            {
                return NotFound();
            }
            return Ok(candidate);
        }

        // PUT: api/Candidates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("updateCanSkill/{id}")]
        public async Task<IActionResult> UpdateCandidateSkill(Guid id, Guid skillId)
        {

            var existingCandidate = await candidatesService.GetCandidateAsync(id);
            var existingSkill = await skillsService.GetSkillAsync(skillId);

            if (existingCandidate == null)
            {
                return NotFound();
            }
            
            if(existingSkill is null)
            {
                return NotFound();
            }


            await candidatesService.UpdateCandidateSkillAsync(id, existingSkill);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSkillAsync([FromQuery] Guid id,  SkillUpdateDTO skill)
        {


            var existingCandidate = await candidatesService.GetCandidateAsync(id);
            var existingSkill = await skillsService.GetSkillAsync(skill.Id);


            if (existingCandidate is null)
            {
                return NotFound();
            }

            
            if(existingSkill is null)
            {
                return NotFound();
            }

            await candidatesService.DeleteSkillAsync(id, existingSkill);
            return NoContent();


        }


        [HttpPut("updateCanInfo")]
        public async Task<IActionResult> UpdateCandidateInfoAsync([FromQuery] Guid id, CandidateDTO candidate)
        {

            var existingCandidate = await candidatesService.GetCandidateAsync(id);
            if(existingCandidate is null)
            {
                return NotFound();
            }
            
            Candidate c = new()
            {
                Name = candidate.Name,
                Email = candidate.Email,
                ContactNumber = candidate.ContactNumber,
                DateOfBirth = candidate.DateOfBirth
            };


            await candidatesService.UpdateInfoAsync(id, c);
            return NoContent();

        }


        [HttpGet("searchCandidates")]
        public async Task<ActionResult> SearchCandidateAsync([FromQuery] string cName)
        {
            var candidates = await candidatesService.SearchCandidatesAsync(cName);
            return Ok(candidates);
        }



        // POST: api/Candidates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CandidateDTO>> PostCandidateAsync(CandidateDTO candidate)
        {

                Candidate newCandidate = new()
                {
                   // Id = candidate.Id,
                    Name = candidate.Name,
                    Email = candidate.Email,
                    ContactNumber = candidate.ContactNumber,
                    DateOfBirth = candidate.DateOfBirth,
                   // Skills = skills
                };

                await candidatesService.CreateCandidateAsync(newCandidate);
                return Ok(candidate);

        }

        // DELETE: api/Candidates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidateAsync(Guid id)
        {

            var candidate = await candidatesService.GetCandidateAsync(id);
            if(candidate is null)
            {
                return NotFound();
            }

            await candidatesService.DeleteCandidateAsync(id);
            return NoContent();

        }

    }
}
