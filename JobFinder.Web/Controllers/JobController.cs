using JobFinder.BLL.Interfaces;
using JobFinder.BLL.Services;
using JobFinder.Core.DTOs;
using JobFinder.DAL.Repositories;
using JobFinder.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Web.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobService _jobService;
        private readonly ILogService<CompanyDTO> _logService;

        public JobController(IJobService jobService, ILogService<CompanyDTO> logService)
        {
            _jobService = jobService;
            _logService = logService;
        }
        public async Task<IActionResult> Jobs()
        {
            var company = await _logService.GetByEmail("utm@gmail.com");
            var jobDTOs = await _jobService.GetJobs();

            var jobViewModels = jobDTOs.Select(job => new JobViewModel()
            {
                Title = job.Title,
                Description = job.Description,
                Requirements = job.Requirements,
                Benefits = job.Benefits,
                Salary = job.Salary,
                Experience = job.Experience,
                City = company.City,
                Studies = job.Studies,
                WorkingType = job.WorkingType,
                CompanyName = company.Name
            }).ToList();

            return View(jobViewModels);
        }
        public IActionResult JobDetails()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddJob()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddJobAsync(JobViewModel jobViewModel)
        {
            if (ModelState.IsValid)
            {
                var jobDto = new JobDTO()
                {
                    Title = jobViewModel.Title,
                    Description = jobViewModel.Description,
                    Requirements = jobViewModel.Requirements,
                    Benefits    = jobViewModel.Benefits,
                    Salary = jobViewModel.Salary,
                    Experience = jobViewModel.Experience,
                    //City = jobViewModel.City,
                    Studies = jobViewModel.Studies,
                    WorkingType = jobViewModel.WorkingType,
                };
                var company = await _logService.GetByEmail("utm@gmail.com");
                jobDto.CompanyId = company.Id;

                await _jobService.AddJob(jobDto);

                RedirectToAction("Jobs");
            }
            return View(jobViewModel);
        }
    }
}
