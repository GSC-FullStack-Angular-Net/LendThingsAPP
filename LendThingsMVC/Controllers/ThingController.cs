using AutoMapper;
using LendThingsAPI.Models;
using LendThingsMVC.Models;
using LendThingsMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace LendThingsMVC.Controllers
{
    public class ThingController : Controller
    {
        private readonly IMapper mapper;

        private readonly IThingService thingService;

        public ThingController(IThingService thingService, IMapper mapper)
        {
            this.thingService = thingService;
            this.mapper = mapper;
        }

        //Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ThingViewModel thingViewModel)
        {
            if (!ModelState.IsValid)
                return View("Create", thingViewModel);

            var entity = mapper.Map<Thing>(thingViewModel);
            thingService.SaveAsync(entity);

            return RedirectToAction(nameof(Index));
        }


        //Retrive
        async public Task<IActionResult> Index()
        {
            var things = await thingService.GetAllAsync();
            var viewModels = mapper.Map<List<ThingViewModel>>(things);
            return View(viewModels);
        }

        async public Task<IActionResult> Details(int id)
        {
            var thing = await thingService.GetByIdAsync(id);
            var viewModel = mapper.Map<ThingViewModel>(thing);
            return View(viewModel);
        }


        //Update
        public async Task<IActionResult> Edit(int id)
        {
            var thing = await thingService.GetByIdAsync(id);
            
            if (thing == null)
            {
                return NotFound();
            }

            return View(mapper.Map<ThingViewModel>(thing));
        }

        [HttpPost] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ThingViewModel thingViewModel)
        {
            if (id != thingViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(thingViewModel);
            }

            thingService.UpdateAsync(mapper.Map<Thing>(thingViewModel));
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

            return View(mapper.Map<ThingViewModel>(thing));
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

            thingService.DeleteAsync(thing);
            return RedirectToAction(nameof(Index));
        }
    }
}
