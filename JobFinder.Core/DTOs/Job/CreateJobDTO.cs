using JobFinder.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Core.DTOs.Job
{
    public class CreateJobDTO : UpdateJobDTO
    {
        public DateTime Created { get; set; }
    }
}
