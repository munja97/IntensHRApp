using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service
{
    public interface ISkillsService
    {
        public Task<IEnumerable<Skill>> GetSkillsAsync();
        public Task<Skill> GetSkillAsync(Guid id);
        public Task CreateSkillAsync(Skill skll);
        public Task UpdateSkillAsync(Skill skill);
        public Task DeleteSkillAsync(Guid id);


    }
}
