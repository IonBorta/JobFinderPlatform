using JobFinder.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Core.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }

/*        public override bool Equals(object obj)
        {
            if (obj is UserDTO userDTO)
            {
                return this.Name == userDTO.Name
                    && this.Email == userDTO.Email
                    && this.UserType == userDTO.UserType;
            }
            return false;
        }*/
    }
}
