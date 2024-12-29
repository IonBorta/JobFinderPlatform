using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Core.Common;
using JobFinder.Core.Enums;
using JobFinder.DAL.Entities;
using Microsoft.AspNetCore.Http;

namespace JobFinder.DAL.State.Interface
{
    public interface IApplicationState
    {
        Task<Result> ReApply(ApplicationEntity jobApplication, IFormFile cvFile);
        Result Withdraw(ApplicationEntity jobApplication);
        Result See(ApplicationEntity jobApplication);
        Result Answer(ApplicationEntity jobApplication, ApplicationJobStates status);
    }
}
