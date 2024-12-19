using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Core.DTOs.Job
{
    public class GetJobDTO: CreateJobDTO
    {
        public string CompanyName { get; set; }
        public string City { get; set; }
    }
}
