using AutoMapper;
using JobFinder.BLL.Interfaces;
using JobFinder.BLL.Services;
using JobFinder.Core.Common;
using JobFinder.Core.DTOs;
using JobFinder.Core.DTOs.Job;
using JobFinder.Core.Enums;
using JobFinder.DAL.Entities;
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
        private readonly JobPageViewModel _jobPageViewModel;

        public JobController(IJobService jobService, IMapper mapper, JobPageViewModel jobPageViewModel)
        {
            _jobService = jobService;
            _mapper = mapper;
            _jobPageViewModel = jobPageViewModel;//new JobPageViewModel();
        }
        public async Task<IActionResult> SortJobs(JobPageViewModel model, FilterCriteria sortCriteria, int value)
        {

/*            var filteredJobs = await _jobService.SortJobs(sortCriteria, value);
            var jobViewModels = filteredJobs.Select(job => _mapper.Map<GetJobViewModel>(job)).ToList();
            return RedirectToAction("Jobs", new { getJobViewModels = jobViewModels });*/
            return RedirectToAction("Jobs", new { sortCriteria, value });
        }
        public async Task<IActionResult> Jobs(JobPageViewModel? model = null /*,SortCriteria? sortCriteria = null, int? value = null*/)
        {
            //_jobPageViewModel.WorkTypeFilter = model.WorkTypeFilter;            //if (sortCriteria.HasValue && value.HasValue)
            if(model != null && model.FilterCriteria != FilterCriteria.None)
            {
                int sortCriteria = (int)model.FilterCriteria;
                bool[] param = model.FilterParams[sortCriteria];
                var filteredJobs = await _jobService.SortJobs(model.FilterCriteria, param);
                var jobs = filteredJobs.Select(job => _mapper.Map<GetJobViewModel>(job)).ToList();
                model.Jobs = jobs;
                return View(model);
            }

            var jobDTOs = await _jobService.GetAll();
            var jobViewModels = jobDTOs.Select(job => _mapper.Map<GetJobViewModel>(job)).ToList();
            model.Jobs = jobViewModels;
            return View(model);

        }
        public async Task<IActionResult> JobsByCompany(int id)
        {
            var jobDTOs = await _jobService.GetJobsByCompany(id);
            var jobViewModels = jobDTOs.Select(job => _mapper.Map<GetJobViewModel>(job)).ToList();
            return View(jobViewModels);
        }
        public async Task<IActionResult> JobDetails(int id)
        {
            var result = await _jobService.GetById(id);
            if (!result.IsSuccess)
            {
                TempData["Message"] = result.ErrorMessage;
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
                var jobDto = _mapper.Map<CreateJobDTO>(createJobViewModel);
                jobDto.CompanyId = companyId;

                var result = await _jobService.Add(jobDto);
                if (!result.IsSuccess)
                {
                    TempData["Message"] = result.ErrorMessage;
                    return View(createJobViewModel);
                }

                return RedirectToAction("Jobs");
            }
            return View(createJobViewModel);
        }
        public async Task<ActionResult> Edit(int id)
        {
            var result = await _jobService.GetById(id);
            if (!result.IsSuccess)
            {
                TempData["Message"] = result.ErrorMessage;
                return View("Error");
            }
            GetJobDTO jobDTO = result.Data;
            var job = _mapper.Map<EditJobViewModel>(jobDTO);
            return View(job);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditJobViewModel editJobViewModel)
        {
            if (ModelState.IsValid)
            {
                var job = _mapper.Map<UpdateJobDTO>(editJobViewModel);
                var result = await _jobService.Update(job);
                if (!result.IsSuccess)
                {
                    TempData["Message"] = result.ErrorMessage;
                    return View(editJobViewModel);
                }
                return RedirectToAction("JobDetails", new {id = job.Id});
            }
            return View(editJobViewModel);
        }
    }
}
