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
    public class SeenState : IApplicationState
    {
        public Result Answer(ApplicationEntity jobApplication, ApplicationJobStates status)
        {
            jobApplication.State = status;
            //jobApplication.State = new AnsweredState();
            return Result.Success();
        }

        public Task<Result> ReApply(ApplicationEntity jobApplication, IFormFile cvFile)
        {
            //throw new InvalidOperationException("Cannot reapply, application is already seen");
            return Task.FromResult(Result.Failure("Only applicants can reapply to an withdrawn job application"));
        }

        public Result See(ApplicationEntity jobApplication)
        {
            //throw new NotImplementedException();
            return Result.Success();
        }

        public Result Withdraw(ApplicationEntity jobApplication)
        {
            //throw new InvalidOperationException("Cannot withdraw, application is already seen");
            return Result.Failure("Only applicants can reapply to an withdrawn job application");
        }
    }
}
