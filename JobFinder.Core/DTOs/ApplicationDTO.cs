using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Core.DTOs
{
    public class ApplicationDTO
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string UserName { get; set; }
        public string JobName { get; set; }
        public int JobId { get; set; }
        public int UserId {  get; set; }
        public string UserEmail { get; set; }
        //public string FilePath { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] FileContent { get; set; }
        public DateTime Submited { get; set; }
    }
}
