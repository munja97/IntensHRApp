using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Data.Repos
{
    public class SkillsRepository : ISkillsRepository
    {
        private readonly HRContext _context;

        public SkillsRepository(HRContext context)
        {
            _context = context;
        }

        public async Task CreateSkillAsync(Skill skill)
        {
             await _context.Skills.AddAsync(skill);
             await _context.SaveChangesAsync();
        }

        public async Task DeleteSkillAsync(Guid id)
        {
            var skill = await _context.Skills.FindAsync(id);
            _context.Remove(skill);
            await _context.SaveChangesAsync();
        }

        

        public async Task<Skill> GetSkillAsync(Guid id)
        {
            return await _context.Skills.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Skill>> GetSkillsAsync()
        {
            return await _context.Skills.ToListAsync();
        }

        public async Task<bool> SkillExistAsync(Guid id)
        {

            var skill = await _context.Skills.FindAsync(id);
            if (skill != null)
                return true;
            else return false;
        }

        public async Task UpdateSkillAsync(Guid id, Skill skill)
        {
            var existingSkill = await _context.Skills.FindAsync(id);

            existingSkill.Name = skill.Name;
            await _context.SaveChangesAsync();
        }
    }
}
