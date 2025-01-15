using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Core.Common;
using JobFinder.Core.DTOs;
using JobFinder.Core.Enums;
using JobFinder.DAL.State.Concrete;
using JobFinder.DAL.State.Interface;
using Microsoft.AspNetCore.Http;

namespace JobFinder.DAL.Entities
{
    public class ApplicationEntity : BaseEntity
    {
        public ApplicationJobStates State { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] FileContent { get; set; }

        // Foreign Keys
        [ForeignKey("Job")]
        public int JobId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        // Navigation Properties
        public virtual Job Job { get; set; }
        public virtual User User { get; set; }
        public virtual Company Company { get; set; }

        [NotMapped]
        bool Updated = false;
        public bool Update(ApplicationEntity entity)
        {
            if(this.State != entity.State)
            {
                this.State = entity.State;
                Updated = true;
            }
            return Updated;
        }

        [NotMapped]
        private IApplicationState ApplicationState 
        {
            get
            {
                return State switch
                {
                    ApplicationJobStates.Pending => new PendingState(),
                    ApplicationJobStates.Withdrawn => new WithdrawnState(),
                    ApplicationJobStates.Seen => new SeenState(),
                    ApplicationJobStates.Accepted or ApplicationJobStates.Rejected => new AnsweredState(),
                    _ => throw new InvalidOperationException("Unknown application status.")
                };
            }
        }

        public Result See()
        {
            return ApplicationState.See(this);
        }
        public Result Withdraw()
        {
            return ApplicationState.Withdraw(this);
        }
        public async Task<Result> Reaply(IFormFile cvFile)
        {
            return await ApplicationState.ReApply(this,cvFile);
        }
        public Result Answer(ApplicationJobStates status)
        {
            return ApplicationState.Answer(this, status);
        }
    }
}
