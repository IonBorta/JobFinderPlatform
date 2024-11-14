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
    public class Company
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string City {  get; set; }
        public string Logo { get; set; }
        public int Workers {  get; set; }
        public CompanyDomains Domain { get; set; }
        [DataType(DataType.Date)]
        public DateTime Created { get; set; }
        public string Password {  get; set; }


        [ForeignKey("User")]
        public int UserId {  get; set; }
        public User User { get; set; } // Navigation property

        // Collection of jobs
        public ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}
