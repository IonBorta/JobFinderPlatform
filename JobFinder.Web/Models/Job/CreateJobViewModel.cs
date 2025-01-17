﻿using System.ComponentModel.DataAnnotations;
using JobFinder.Core.Enums;

namespace JobFinder.Web.Models.Job
{
    public class CreateJobViewModel: BaseViewModel
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public required string Description { get; set; }
        [Required(ErrorMessage = "Salary is required")]
        public int Salary { get; set; }
        [Required(ErrorMessage = "Requirements are required")]
        public string Requirements { get; set; }
        [Required(ErrorMessage = "Benefits are required")]
        public string Benefits { get; set; }
        [Required(ErrorMessage = "Experience is required")]
        public WorkExperience Experience { get; set; }
        [Required(ErrorMessage = "Studies are required")]
        public StudiesLevel Studies { get; set; }
        [Required(ErrorMessage = "WorkingType is required")]
        public WorkingType WorkingType { get; set; }
        [Required(ErrorMessage = "Posted is required")]
        public int CompanyId { get; set; }
    }
}
