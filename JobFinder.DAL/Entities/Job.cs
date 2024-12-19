using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Core.Enums;

namespace JobFinder.DAL.Entities
{
    public class Job: BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Salary { get; set; }
        public string Requirements { get; set; }
        public string Benefits { get; set; }
        public WorkExperience Experience { get; set; }
        public StudiesLevel Studies { get; set; }
        public WorkingType WorkingType { get; set; }
        // Foreign Keys
        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        // Navigation Properties
        public virtual Company Company { get; set; }

        [NotMapped]
        bool Updated = false;
        public bool Update(Job job)
        {
            if(this.Title != job.Title)
            {
                this.Title = job.Title;
                Updated = true;
            }
            if (this.Description != job.Description)
            {
                this.Description = job.Description;
                Updated = true;
            }
            if (this.Salary != job.Salary)
            {
                this.Salary = job.Salary;
                Updated = true;
            }
            if (this.Requirements != job.Requirements)
            {
                this.Requirements = job.Requirements;
                Updated = true;
            }
            if (this.Benefits != job.Benefits)
            {
                this.Benefits = job.Benefits;
                Updated = true;
            }
            if (this.Experience != job.Experience)
            {
                this.Experience = job.Experience;
                Updated = true;
            }
            if (this.Studies != job.Studies)
            {
                this.Studies = job.Studies;
                Updated = true;
            }
            if (this.WorkingType != job.WorkingType)
            {
                this.WorkingType = job.WorkingType;
                Updated = true;
            }
            
            return Updated;
        }
    }
}
