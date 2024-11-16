using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.DAL.Entities
{
    public class Application
    {
        [Key]
        public int Id { get; set; }
        //[ForeignKey("Job")]
        public int JobId { get; set; }
        //[ForeignKey("User")]
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        //public string FilePath { get; set; }
        public DateTime Submitted { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] FileContent { get; set; }

/*        // Navigation properties (optional)
        public Job Job { get; set; }
        public User User { get; set; }*/

        [NotMapped]
        public string CompanyName { get; set; }
        public string UserName { get; set;}
        [NotMapped]
        public string UserEmail { get; set;}
        [NotMapped]
        public string JobName { get; set; } 
    }
}
