﻿using JobFinder.Core.Common;
using JobFinder.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.BLL.Interfaces
{
    public interface IApplicationService: IBaseService<ApplicationDTO>
    {
        Task<IList<ApplicationDTO>> GetApplicationsByCompany(int id);
    }
}
