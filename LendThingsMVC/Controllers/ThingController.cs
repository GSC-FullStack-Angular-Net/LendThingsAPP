﻿using AutoMapper;
using LendThingsCommonClasses.DTO;
using LendThingsMVC.Models;
using LendThingsMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace LendThingsMVC.Controllers
{
    public class ThingController : Controller
    {
        private readonly IMapper mapper;
        private readonly IThingService thingService;
        private readonly ICategoryService categoryService;

        public ThingController(IThingService thingService,ICategoryService categoryService, IMapper mapper)
        {
            this.thingService = thingService;
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        //Retrive
        async public Task<IActionResult> Index()
        {
            var things = await thingService.GetAllFullAsync();
            var viewModels = mapper.Map<List<ThingViewModel>>(things);
            return View(viewModels);
        }

        async public Task<IActionResult> Details(int id)
        {
            var thing = await thingService.GetByIdAsync(id);
            var viewModel = mapper.Map<ThingViewModel>(thing);
            return View(viewModel);
        }

        //Create
        async public Task<IActionResult> Create()
        {
            ViewBag.CategoryList = await categoryService.GetAllBaseAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ThingForCreationViewModel thingViewModel)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Create));

            var newThing = mapper.Map<ThingForCreationDTO>(thingViewModel);
            await thingService.SaveAsync(newThing);
            return RedirectToAction(nameof(Index));
        }




        //Update
        public async Task<IActionResult> Edit(int id)
        {
            var thing = await thingService.GetByIdAsync(id);
            
            if (thing == null)
            {
                return NotFound();
            }
            ViewBag.Categories = await categoryService.GetAllBaseAsync();
            var thingViewModel = mapper.Map<ThingForCreationViewModel>(thing);
            return View(thingViewModel);
        }

        [HttpPost] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ThingForCreationViewModel thingViewModel)
        {
            if (id != thingViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(thingViewModel);
            }
            var thingDTO = mapper.Map<ThingBaseDTO>(thingViewModel);
            await thingService.UpdateAsync(thingDTO);
            return RedirectToAction(nameof(Index));
        }


        //Delete
        public async Task<IActionResult> Delete(int id)
        {
            var thing = await thingService.GetByIdAsync(id);
            if (thing == null)
            {
                return NotFound();
            }

            var thingViewModel = mapper.Map<ThingViewModel>(thing);

            return View(thingViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteOperation(int id)
        {
            var thing = await thingService.GetByIdAsync(id);
            if (thing is null)
            {
                return NotFound();
            }
            var thingDTO = mapper.Map<ThingBaseDTO>(thing);
            await thingService.DeleteAsync(thingDTO);
            return RedirectToAction(nameof(Index));
        }
    }
}
