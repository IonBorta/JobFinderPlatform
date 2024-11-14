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

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }
        public async Task<IActionResult> Jobs()
        {
            var jobDTOs = await _jobService.GetJobs();

            var jobViewModels = jobDTOs.Select(job => new JobViewModel()
            {
                Title = job.Title,
                Description = job.Description,
                Requirements = job.Requirements,
                Benefits = job.Benefits,
                Salary = job.Salary,
                Experience = job.Experience,
                //City = job.City,
                Studies = job.Studies,
                WorkingType = job.WorkingType
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

                await _jobService.AddJob(jobDto);

                RedirectToAction("Jobs");
            }
            return View(jobViewModel);
        }
    }
}
