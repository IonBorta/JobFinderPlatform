using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.DAL.Entities
{
    public class Application : BaseEntity
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] FileContent { get; set; }

        // Foreign Keys
        [ForeignKey("Jobs")]
        public int JobId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        // Navigation Properties
        public virtual Job Job { get; set; }
        public virtual User User { get; set; }
        public virtual Company Company { get; set; }
    }
}
