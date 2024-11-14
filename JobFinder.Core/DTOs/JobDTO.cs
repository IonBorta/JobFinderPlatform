using JobFinder.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Core.DTOs
{
    public class JobDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Salary { get; set; }
        public string Requirements { get; set; }
        public string Benefits { get; set; }
        public WorkExperience Experience { get; set; }
        public string City { get; set; }
        public StudiesLevel Studies { get; set; }
        public WorkingType WorkingType { get; set; }
        public DateTime Posted { get; set; }

        // Conversion from JobViewModel to JobDTO
/*        public static JobDTO FromViewModel(JobViewModel viewModel)
        {
            return new JobDTO
            {
                Title = viewModel.Title,
                Description = viewModel.Description,
                Requirements = viewModel.Requirements,
                Benefits = viewModel.Benefits,
                Salary = viewModel.Salary,
                Experience = viewModel.Experience,
                City = viewModel.City,
                Studies = viewModel.Studies,
                WorkingType = viewModel.WorkingType
            };
        }

        // Conversion from Job to JobDTO
        public static JobDTO FromModel(Job model)
        {
            return new JobDTO
            {
                Title = model.Title,
                Description = model.Description,
                Requirements = model.Requirements,
                Benefits = model.Benefits,
                Salary = model.Salary,
                Experience = model.Experience,
                City = model.City,
                Studies = model.Studies,
                WorkingType = model.WorkingType
            };
        }

        // Conversion to Job model
        public Job ToModel()
        {
            return new Job
            {
                Title = this.Title,
                Description = this.Description,
                Requirements = this.Requirements,
                Benefits = this.Benefits,
                Salary = this.Salary,
                Experience = this.Experience,
                City = this.City,
                Studies = this.Studies,
                WorkingType = this.WorkingType
            };
        }*/
    }
}
