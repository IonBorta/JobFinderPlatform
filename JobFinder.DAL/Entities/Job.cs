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
    public class Job
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public string Benefits { get; set; }
        public decimal Salary { get; set; } = 0;
        public WorkExperience Experience { get; set; }
        public string City { get; set; }
        public StudiesLevel Studies {  get; set; }
        public WorkingType WorkingType {  get; set; }
        [DataType(DataType.Date)]
        public DateTime Posted { get; set; }
        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
