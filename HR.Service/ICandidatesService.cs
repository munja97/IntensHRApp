﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service
{
   public interface ICandidatesService
    {
        public Task<IEnumerable<Candidate>> GetCandidatesAsync();
        public Task<Candidate> GetCandidateAsync(Guid id);
        public Task CreateCandidateAsync(Candidate candidate);
        public Task UpdateCandidateSkillAsync(Guid id, Skill skill);
        public Task DeleteCandidateAsync(Guid id);
        public Task UpdateInfoAsync(Guid id, Candidate candidate);
        public Task DeleteSkillAsync(Guid id, Skill skill);
        public Task<IEnumerable<Candidate>> SearchCandidatesAsync(string cName);

    }
}
