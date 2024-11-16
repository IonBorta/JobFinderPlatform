using JobFinder.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Core.DTOs
{
    public class CompanyDTO
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public int Workers {  get; set; }
        public string Logo { get; set; }
        public string PhoneNumber {  get; set; }
        public CompanyDomains Domain {  get; set; }
        public string City { get; set; }
        public string Password {  get; set; }
        public int JobsCount { get; set; }

    }
}
