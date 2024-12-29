using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Core.Enums;

namespace JobFinder.Core.DTOs
{
    public class CreateApplicationDTO
    {
        public int JobId { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] FileContent { get; set; }
        public DateTime Created { get; set; }
        public ApplicationJobStates States { get; set; }
    }
    public class UpdateApplicationDTO
    {
        public int Id { get; set; }
        public ApplicationJobStates States { get; set; }
    }
    public class GetApplicationDTO
    {
        public int Id { get; set; }
        public ApplicationJobStates States { get; set; }
        public string CompanyName { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string JobName { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] FileContent { get; set; }
        public DateTime Submited { get; set; }

    }
    public class ApplicationDTO
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public string JobName { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] FileContent { get; set; }
        public DateTime Created { get; set; }
        public ApplicationJobStates State { get; set; }
    }
}
