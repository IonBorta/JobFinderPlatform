using JobFinder.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.DAL.Entities
{
    public class Job: BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public string Benefits { get; set; }
        public int Salary { get; set; } = 0;
        public WorkExperience Experience { get; set; }
        public StudiesLevel Studies {  get; set; }
        public WorkingType WorkingType {  get; set; }

        // Foreign Keys
        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        // Navigation Properties
        public virtual Company Company { get; set; }
    }
}
