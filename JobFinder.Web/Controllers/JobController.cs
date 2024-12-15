using AutoMapper;
using JobFinder.BLL.Interfaces;
using JobFinder.BLL.Services;
using JobFinder.Core.Common;
using JobFinder.Core.DTOs;
using JobFinder.DAL.Entities;
using JobFinder.DAL.Repositories;
using JobFinder.Web.Models;
using JobFinder.Web.Models.Job;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JobFinder.Web.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobService _jobService;
        private readonly IMapper _mapper;

        public JobController(IJobService jobService, IMapper mapper)
        {
            _jobService = jobService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Jobs()
        {


            var jobDTOs = await _jobService.GetJobs();


            var jobViewModels = jobDTOs.Select(job => _mapper.Map<GetJobViewModel>(job)).ToList();

            return View(jobViewModels);
        }
        public async Task<IActionResult> JobsByCompany(int id)
        {
            var jobDTOs = await _jobService.GetJobsByCompany(id);
            var jobViewModels = jobDTOs.Select(job => _mapper.Map<GetJobViewModel>(job)).ToList();
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
            var job = _mapper.Map<GetJobViewModel>(jobDTO);

            return View(job);
        }
        [HttpGet]
        public IActionResult AddJob(int id)
        {
            TempData["CompanyId"] = id;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddJobAsync(CreateJobViewModel createJobViewModel)
        {
            if (ModelState.IsValid)
            {
                int companyId = Convert.ToInt32(TempData["CompanyId"]);
                var jobDto = _mapper.Map<JobDTO>(createJobViewModel);
                jobDto.CompanyId = companyId;

                var result = await _jobService.AddJob(jobDto);
                if (!result.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty, result.ErrorMessage);
                    return View(createJobViewModel);
                }

                return RedirectToAction("Jobs");
            }
            return View(createJobViewModel);
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
            var job = _mapper.Map<GetJobViewModel>(jobDTO);
            return View(job);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditJobViewModel editJobViewModel)
        {
            if (ModelState.IsValid)
            {
                var job = _mapper.Map<JobDTO>(editJobViewModel);
                var result = await _jobService.UpdateJob(job);
                if (!result.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty, result.ErrorMessage);
                    return View(editJobViewModel);
                }
                return RedirectToAction("JobDetails", new {id = job.Id});
            }
            return View(editJobViewModel);
        }
    }
}
