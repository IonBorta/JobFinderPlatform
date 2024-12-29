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
    public class AnsweredState : IApplicationState
    {
        public Result Answer(ApplicationEntity jobApplication, ApplicationJobStates status)
        {
            //throw new NotImplementedException();
            return Result.Failure($"You already answered to this job application");
        }

        public Task<Result> ReApply(ApplicationEntity jobApplication, IFormFile cvFile)
        {
            //throw new NotImplementedException();
            return Task.FromResult(Result.Failure($"Can't reaply to an answered job application"));
        }

        public Result See(ApplicationEntity jobApplication)
        {
            //throw new NotImplementedException();
            return Result.Success();
        }

        public Result Withdraw(ApplicationEntity jobApplication)
        {
            //throw new NotImplementedException();
            return Result.Failure($"Can't withdraw an answered job application");
        }
    }
}
