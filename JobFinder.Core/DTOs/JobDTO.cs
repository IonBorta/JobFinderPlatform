using JobFinder.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Core.DTOs
{
    public class JobDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Salary { get; set; }
        public string Requirements { get; set; }
        public string Benefits { get; set; }
        public WorkExperience Experience { get; set; }
        public string City { get; set; }
        public StudiesLevel Studies { get; set; }
        public WorkingType WorkingType { get; set; }
        public DateTime Posted { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }

    }
}
