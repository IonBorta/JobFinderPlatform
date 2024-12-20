﻿using JobFinder.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.DAL.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [DataType(DataType.Date)]
        public DateTime Created { get; set; }
        public UserType UserType { get; set; } = UserType.Employee;
    }
}
