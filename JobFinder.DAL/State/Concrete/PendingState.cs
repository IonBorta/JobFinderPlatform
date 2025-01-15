using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Core.Common;
using JobFinder.Core.Enums;
using JobFinder.DAL.Entities;
using JobFinder.DAL.State.Interface;
using Microsoft.AspNetCore.Http;

namespace JobFinder.DAL.State.Concrete
{
    public class PendingState : IApplicationState
    {
        public Result Answer(ApplicationEntity jobApplication, ApplicationJobStates status)
        {
            return Result.Failure($"Job application must be seen to answer to it");
        }
        public Task<Result> ReApply(ApplicationEntity jobApplication, IFormFile cvFile)
        {
            return Task.FromResult(Result.Failure($"Job application must be withdrawn to reapply to it"));
        }
        public Result See(ApplicationEntity jobApplication)
        {
            jobApplication.State = ApplicationJobStates.Seen;
            return Result.Success();
        }
        public Result Withdraw(ApplicationEntity jobApplication)
        {
            jobApplication.State = ApplicationJobStates.Withdrawn;
            return Result.Success();
        }
    }
}
