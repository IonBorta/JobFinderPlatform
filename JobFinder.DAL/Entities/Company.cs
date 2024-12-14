using JobFinder.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.DAL.Entities
{
    public class Company: BaseEntity
    {
        public string? Description { get; set; }
        public string PhoneNumber { get; set; }
        public string? City {  get; set; }
        public string? Logo { get; set; }
        public int? WorkersCount {  get; set; }
        public CompanyDomains Domain { get; set; }

        // Foreign Keys
        [ForeignKey("User")]
        public int UserId {  get; set; }

        // Navigation property
        public virtual User User { get; set; } 
        public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}
