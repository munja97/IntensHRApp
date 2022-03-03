using HR.Data.Repos;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service
{
    public class CandidatesService : ICandidatesService
    {

        private readonly ICandidatesRepository repo;
        private readonly ISkillsRepository skRepo;

        public CandidatesService(ICandidatesRepository canRepo,ISkillsRepository skillRepo)
        {
            repo = canRepo;
            skRepo = skillRepo;
        }

        public async Task CreateCandidateAsync(Candidate candidate)
        {
           await repo.CreateCandidateAsync(candidate);


        }

        public async Task DeleteCandidateAsync(Guid id)
        {
           await repo.DeleteCandidateAsync(id);
        }

        public async Task<Candidate> GetCandidateAsync(Guid id)
        {
            return await repo.GetCandidateAsync(id);
        }

        public async Task< IEnumerable<Candidate>> GetCandidatesAsync()
        {
            return await repo.GetCandidatesAsync(); 
        }



        public async Task UpdateInfoAsync(Guid id, Candidate candidate)
        {

            await repo.UpdateInfoAsync(id, candidate);
            



        }

        public async Task UpdateCandidateSkillAsync(Guid id, Skill skill)
        {
            await repo.UpdateCandidateSkillAsync(id, skill);
     
        }

        public async Task DeleteSkillAsync(Guid id, Skill skill)
        {

                await repo.DeleteSkillAsync(id, skill);
        }

        public async Task< IEnumerable<Candidate>> SearchCandidatesAsync(string cName)
        {
            return await repo.SearchCandidateAsync(cName);
        }

        
    }
}
