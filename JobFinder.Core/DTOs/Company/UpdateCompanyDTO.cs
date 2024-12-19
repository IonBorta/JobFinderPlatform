using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Core.DTOs.User;
using JobFinder.Core.Enums;

namespace JobFinder.Core.DTOs.Company
{
    public class UpdateCompanyDTO
    {
        public UpdateUserDTO UpdateUser {  get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public int WorkersCount { get; set; }
        public string Logo { get; set; }
        public string PhoneNumber { get; set; }
        public CompanyDomains Domain { get; set; }
        public string City { get; set; }
    }
}
