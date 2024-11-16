using JobFinder.BLL.Interfaces;
using JobFinder.BLL.Services;
using JobFinder.Core.Common;
using JobFinder.Core.DTOs;
using JobFinder.DAL.Entities;
using JobFinder.DAL.Repositories;
using JobFinder.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                Requirements = job.Requirements,
                Benefits = job.Benefits,
                Salary = job.Salary,
                Experience = job.Experience,
                City = job.City,
                Studies = job.Studies,
                WorkingType = job.WorkingType,
                CompanyId = job.CompanyId,
                CompanyName = job.CompanyName,

            }).ToList();

            return View(jobViewModels);
        }
        public async Task<IActionResult> JobsByCompany(int id)
        {
            var jobDTOs = await _jobService.GetJobsByCompany(id);
            var jobViewModels = jobDTOs.Select(job => new JobViewModel()
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                Requirements = job.Requirements,
                Benefits = job.Benefits,
                Salary = job.Salary,
                Experience = job.Experience,
                City = job.City,
                Studies = job.Studies,
                WorkingType = job.WorkingType,
                CompanyId = job.CompanyId,
                CompanyName = job.CompanyName,

            }).ToList();
            return View(jobViewModels);
        }
        public async Task<IActionResult> JobDetails(int id)
        {
            var result = await _jobService.GetJobById(id);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage);
                return View("Error");
            }
            var jobDTO = result.Data;
            var job = new JobViewModel()
            {
                Id = jobDTO.Id,
                Title = jobDTO.Title,
                Description = jobDTO.Description,
                Requirements = jobDTO.Requirements,
                Benefits = jobDTO.Benefits,
                Salary = jobDTO.Salary,
                Experience = jobDTO.Experience,
                Studies = jobDTO.Studies,
                WorkingType = jobDTO.WorkingType,
                CompanyId = jobDTO.CompanyId,
                Posted = jobDTO.Posted,
                CompanyName = jobDTO.CompanyName,
                City = jobDTO.City,
            };


            return View(job);
        }
        [HttpGet]
        public IActionResult AddJob(int id)
        {
            TempData["CompanyId"] = id;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddJobAsync(JobViewModel jobViewModel)
        {
            if (ModelState.IsValid)
            {
                int companyId = Convert.ToInt32(TempData["CompanyId"]);
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
                    CompanyId = companyId
                };

                var result = await _jobService.AddJob(jobDto);
                if (!result.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty, result.ErrorMessage);
                    return View(jobViewModel);
                }

                return RedirectToAction("Jobs");
            }
            return View(jobViewModel);
        }
        public async Task<ActionResult> Edit(int id)
        {
            var result = await _jobService.GetJobById(id);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage);
                return View("Error");
            }
            JobDTO jobDTO = result.Data;
            var job = new JobViewModel()
            {
                Id = jobDTO.Id,
                Title = jobDTO.Title,
                Description = jobDTO.Description,
                Requirements = jobDTO.Requirements,
                Benefits= jobDTO.Benefits,
                Salary = jobDTO.Salary,
                Experience = jobDTO.Experience,
                WorkingType= jobDTO.WorkingType,
                Studies= jobDTO.Studies,
            };
            return View(job);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(JobViewModel jobViewModel)
        {
            if (ModelState.IsValid)
            {
                var job = new JobDTO(){
                    Id = jobViewModel.Id,
                    Title = jobViewModel.Title,
                    Description = jobViewModel.Description,
                    Requirements = jobViewModel.Requirements,
                    Benefits = jobViewModel.Benefits,
                    Salary = jobViewModel.Salary,
                    Experience = jobViewModel.Experience,
                    WorkingType = jobViewModel.WorkingType,
                    Studies = jobViewModel.Studies,
                };
                var result = await _jobService.UpdateJob(job);
                if (!result.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty, result.ErrorMessage);
                    return View(jobViewModel);
                }
                return RedirectToAction("JobDetails", new {id = job.Id});
            }
            return View(jobViewModel);
        }
    }
}
