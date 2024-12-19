using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Core.Enums;
using JobFinder.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.DAL.Entities
{
    public class User:BaseEntity
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }

        [NotMapped]
        bool Updated = false;
        public bool Update(User user)
        {
            if (this.Email != user.Email) 
            { 
                this.Email = user.Email;
                Updated = true;
            }
            if(this.Name != user.Name)
            {
                this.Name = user.Name;
                Updated = true;
            }
            return Updated;
        }
    }
}
