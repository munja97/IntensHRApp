using HR.Data.Repos;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service
{
    public class SkillsService : ISkillsService
    {
        private readonly ISkillsRepository repo;

        public SkillsService(ISkillsRepository skillRepo)
        {
            repo = skillRepo;
        }

        public async Task CreateSkillAsync(Skill skll)
        {
           await repo.CreateSkillAsync(skll);
        }

        public async Task DeleteSkillAsync(Guid id)
        {
           await repo.DeleteSkillAsync(id);      
        }

        

        public async Task< Skill> GetSkillAsync(Guid id)
        {
            return await repo.GetSkillAsync(id);

        }

        public async Task< IEnumerable<Skill>> GetSkillsAsync()
        {
            return await repo.GetSkillsAsync();
        }


        public async Task UpdateSkillAsync(Skill skill)
        {
                await repo.UpdateSkillAsync(skill.Id, skill);
                

        }
    }
}
