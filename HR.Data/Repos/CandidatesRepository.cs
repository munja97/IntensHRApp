using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Data.Repos
{
    public class CandidatesRepository : ICandidatesRepository
    {
        private readonly HRContext _context;

        public CandidatesRepository(HRContext context)
        {
            _context = context;
        }
        public async Task CreateCandidateAsync(Candidate candidate)
        {
            await _context.Candidates.AddAsync(candidate);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCandidateAsync(Guid id)
        {
            var candidate = await _context.Candidates.FindAsync(id);
            _context.Remove(candidate);
            await _context.SaveChangesAsync();
        }

        


        public async Task<Candidate> GetCandidateAsync(Guid id)
        {

            
            return await _context.Candidates.FirstOrDefaultAsync(c => c.Id == id);
        }
    

        public async Task< IEnumerable<Candidate>> GetCandidatesAsync()
        {
            return await _context.Candidates.Include(c => c.Skills).ToListAsync();

            
        }

        public async Task UpdateCandidateSkillAsync(Guid id, Skill skill)
        {
            var existingCandidate = await _context.Candidates.FindAsync(id);
            existingCandidate.Skills = new List<Skill>();
            existingCandidate.Skills.Add(skill);

          await _context.SaveChangesAsync();
        }


        public async Task UpdateInfoAsync(Guid id, Candidate candidate)
        {
            var existingCandidate = await _context.Candidates.FindAsync(id);


            existingCandidate.Name = candidate.Name;
            existingCandidate.Email = candidate.Email;
            existingCandidate.ContactNumber = candidate.ContactNumber;
            existingCandidate.DateOfBirth = candidate.DateOfBirth;
            await _context.SaveChangesAsync();

        }

        public async Task DeleteSkillAsync(Guid id, Skill skill)
        {
            var existingCandidate = await _context.Candidates.FindAsync(id);

            existingCandidate.Skills.Remove(skill);
            await _context.SaveChangesAsync();

        }



        
        public async Task<IEnumerable<Candidate>> SearchCandidateAsync(string cName)
        {
            if (string.IsNullOrEmpty(cName))
            {
                return await _context.Candidates.Include(c => c.Skills).ToListAsync();
            }

            return await _context.Candidates.Include(c => c.Skills)
                .Where(c => c.Name.Contains(cName)).ToListAsync();

        }

    }
}
