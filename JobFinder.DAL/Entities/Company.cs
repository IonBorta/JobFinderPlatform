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
    public class Company: BaseEntity
    {
        public string? Description { get; set; }
        public int? WorkersCount { get; set; }
        public string? Logo { get; set; }
        public string PhoneNumber { get; set; }
        public CompanyDomains Domain { get; set; }
        public string? City { get; set; }
        // Foreign Keys
        [ForeignKey("User")]
        public int UserId { get; set; }

        // Navigation property
        public virtual User User { get; set; }
        public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();


        [NotMapped]
        bool Updated = false;
        public bool Update(Company company)
        {
            if (this.Description != company.Description)
            {
                this.Description = company.Description;
                Updated = true;
            }
            if (this.WorkersCount != company.WorkersCount)
            {
                this.WorkersCount = company.WorkersCount;
                Updated = true;
            }
            if (this.Logo != company.Logo)
            {
                this.Logo = company.Logo;
                Updated = true;
            }
            if (this.PhoneNumber != company.PhoneNumber)
            {
                this.PhoneNumber = company.PhoneNumber;
                Updated = true;
            }
            if (this.Domain != company.Domain)
            {
                this.Domain = company.Domain;
                Updated = true;
            }
            if (this.City != company.City)
            {
                this.City = company.City;
                Updated = true;
            }
            return Updated;
        }
    }
}
