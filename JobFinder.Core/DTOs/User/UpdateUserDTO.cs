﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Core.DTOs.User
{
    public class UpdateUserDTO
    {
        public int Id { get; set; }
        public string Email {  get; set; }
        public string Name { get; set; }
    }
}
