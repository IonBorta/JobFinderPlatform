using JobFinder.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Core.DTOs.User
{
    public class CreateUserDTO: GetUserDTO
    {
        public string Password { get; set; }
    }
}
