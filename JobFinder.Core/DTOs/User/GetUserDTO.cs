using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Core.Enums;

namespace JobFinder.Core.DTOs.User
{
    public class GetUserDTO: UpdateUserDTO
    {
        public UserType UserType { get; set; }
        public DateTime Created { get; set; }
    }
}
