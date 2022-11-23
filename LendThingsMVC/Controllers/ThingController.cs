using AutoMapper;
using LendThingsCommonClasses.DTO;
using LendThingsMVC.Models;
using LendThingsMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LendThingsMVC.Controllers
{
    public class ThingController : BaseCRUDController
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
            var response = await thingService.GetAllFullAsync();
            if (response.Body is null)
            {
                return ManageServiceErrorResponse(response.Response);
            }
            var viewModels = mapper.Map<List<ThingViewModel>>(response.Body);
            return View(viewModels);
        }

        async public Task<IActionResult> Details(int id)
        {
            var response = await thingService.GetByIdAsync(id);
            if (response.Body is null)
            {
                return ManageServiceErrorResponse(response.Response);   
            }
            var viewModel = mapper.Map<ThingViewModel>(response.Body);
            return View(viewModel);
        }

        //Create
        async public Task<IActionResult> Create()
        {
            var response = await categoryService.GetAllBaseAsync();
            if (response.Body is null)
            {
                return ManageServiceErrorResponse(response.Response);
            }
            ViewBag.CategoryList =  response.Body;
            return View();
        }


        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOperation(ThingCreationViewModel thingViewModel)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Create));

            var newThing = mapper.Map<ThingForCreationDTO>(thingViewModel);
            var response = await thingService.SaveAsync(newThing);
            if (response.PersonalizedError is not null)
            {
                ModelState.AddModelError("Description", response.PersonalizedError);
                //Forma de volver a pasar la view pero sin tener que pedir las categorias??(El redirect no funciona porque resetea el ModelState)
                var responseCategories = await categoryService.GetAllBaseAsync();
                if (!responseCategories.Response.IsSuccessStatusCode)
                {
                    return ManageServiceErrorResponse(response.Response);
                }
                ViewBag.CategoryList = responseCategories.Body;
                return View(thingViewModel);
            }

            if (!response.Response.IsSuccessStatusCode)
            {
                return ManageServiceErrorResponse(response.Response);
            }
            
            return RedirectToAction(nameof(Details),new { id=response!.Body!.Id });
        }




        //Update
        public async Task<IActionResult> Edit(int id)
        {
            var response = await thingService.GetByIdAsync(id);

            if (response.Body is null)
            {
                return ManageServiceErrorResponse(response.Response);
            }
            var responseCategory = await categoryService.GetAllBaseAsync();
            if (responseCategory.Body is null)
            {
                return ManageServiceErrorResponse(response.Response);
            }
            ViewBag.CategoryList = responseCategory.Body;
            var thingViewModel = mapper.Map<ThingEditViewModel>(response.Body);
            return View(thingViewModel);
        }

        [HttpPost] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ThingEditViewModel thingViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(thingViewModel);
            }

            var thingDTO = mapper.Map<ThingBaseDTO>(thingViewModel);
            thingDTO.Id= id;
            var response = await thingService.UpdateAsync(thingDTO);
            if (response.PersonalizedError is not null)
            {
                ModelState.AddModelError("Description", response.PersonalizedError);
                //Forma de volver a pasar la view pero sin tener que pedir las categorias??(El redirect no funciona porque resetea el ModelState)
                var responseCategories = await categoryService.GetAllBaseAsync();
                if (!responseCategories.Response.IsSuccessStatusCode)
                {
                    return ManageServiceErrorResponse(response.Response);
                }
                ViewBag.CategoryList = responseCategories.Body;
                return View(thingViewModel);
            }
            if (response.Body is null)
            {
                return ManageServiceErrorResponse(response.Response);
            }
            return RedirectToAction(nameof(Index));
        }


        //Delete
        public async Task<IActionResult> Delete(int id)
        {
            var response = await thingService.GetByIdAsync(id);
            if (response.Body is null)
            {
                return ManageServiceErrorResponse(response.Response);
            }

            var thingViewModel = mapper.Map<ThingViewModel>(response.Body);

            return View(thingViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteOperation(int id)
        {
            var response = await thingService.GetByIdAsync(id);
            if (response.Body is null)
            {
                return ManageServiceErrorResponse(response.Response);
            }
            var thingDTO = mapper.Map<ThingBaseDTO>(response.Body);
            var responseDelete = await thingService.DeleteAsync(thingDTO);
            if (responseDelete.Body is null)
            {
                return ManageServiceErrorResponse(response.Response);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
