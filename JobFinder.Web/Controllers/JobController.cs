﻿using AutoMapper;
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

        public JobController(IJobService jobService, IMapper mapper)
        {
            _jobService = jobService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Jobs(JobPageViewModel? model = null)
        {
            
            if(model != null && model.ToFilter)
            {
                var jobsDTO = await _jobService.GetAll();
                foreach (var filter in model.FilterCriterias)
                {
                    _jobService.SetStrategy(filter.Type);
                    jobsDTO = _jobService.FilterJobs(jobsDTO, filter.FilterParams);
                }
                //var filteredJobs = await _jobService.SortJobs(model.FilterCriterias);
                var jobs = jobsDTO.Select(job => _mapper.Map<GetJobViewModel>(job)).ToList();
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
                jobDto.Id = 0;

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
