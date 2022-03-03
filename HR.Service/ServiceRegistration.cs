using HR.Data.Repos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service
{
    public static class ServiceRegistration
    {
        public static void AddNewServices(this IServiceCollection services)
        {
            services.AddScoped<ISkillsRepository, SkillsRepository>();
            services.AddScoped<ICandidatesRepository, CandidatesRepository>();
        }
    }
}
