using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JobFinder.Core.Common;
using JobFinder.Core.Enums;
using JobFinder.DAL.Entities;
using JobFinder.DAL.State.Interface;
using Microsoft.AspNetCore.Http;

namespace JobFinder.DAL.State.Concrete
{
    public class WithdrawnState : IApplicationState
    {
        public Result Answer(ApplicationEntity jobApplication,ApplicationJobStates status)
        {
            //throw new InvalidOperationException("Cannot answer to a withdrawn application");
            return Result.Failure("Companies can answer only to seen job applications");
        }

        public async Task<Result> ReApply(ApplicationEntity jobApplication,IFormFile cvFile)
        {
            jobApplication.State = ApplicationJobStates.Pending;
            //jobApplication.State = new PendingState();
            using (var memoryStream = new MemoryStream())
            {
                await cvFile.CopyToAsync(memoryStream);
                jobApplication.FileContent = memoryStream.ToArray();
                jobApplication.FileName = cvFile.FileName;
                jobApplication.ContentType = cvFile.ContentType;
            }
            return Result.Success();
        }

        public Result See(ApplicationEntity jobApplication)
        {
            //throw new NotImplementedException();
            return Result.Failure("Not found. CV is withdrawn.");
        }

        public Result Withdraw(ApplicationEntity jobApplication)
        {
            //throw new InvalidOperationException("Application is already withdrawn");
            return Result.Failure("Job application is already withdrawn");
        }
    }
}
