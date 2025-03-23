using ESFE.BusinessLogic.DTOs;
using ESFE.BusinessLogic.UseCases.Brands.Commands.CreateBrand;
using ESFE.BusinessLogic.UseCases.Brands.Commands.DeleteBrand;
using ESFE.BusinessLogic.UseCases.Brands.Commands.UpdateBrand;
using ESFE.BusinessLogic.UseCases.Brands.Queries.GetBrand;
using ESFE.BusinessLogic.UseCases.Brands.Queries.GetBrands;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESFE.WebApplication.Controllers
{
    [Authorize]
    public class BrandController : Controller
    {
        private readonly IMediator _mediator;

        public BrandController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IActionResult> Index()
        {
            var brands = await _mediator.Send(new GetBrandsQuery());
            return View(brands);
        }        

        // GET: BrandController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BrandController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBrandRequest createBrandRequest)
        {
            try
            {
                var result= await _mediator.Send(new CreateBrandCommand(createBrandRequest));               
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    throw new Exception("Sucedio un error al intentar guardar la nueva marca");              
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(createBrandRequest);
            }
        }

        // GET: BrandController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var brand = await _mediator.Send(new GetBrandQuery(id));
            return View(brand.Adapt(new UpdateBrandRequest()));
        }

        // POST: BrandController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateBrandRequest updateBrandRequest)
        {
            try
            {
                var result = await _mediator.Send(new UpdateBrandCommand(updateBrandRequest));
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    throw new Exception("Sucedio un error al intentar editar marca");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(updateBrandRequest);
            }
        }

        // GET: BrandController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _mediator.Send(new GetBrandQuery(id));
            return View(brand);
        }

        // POST: BrandController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, BrandResponse brandResponse)
        {
            try
            {
                var result = await _mediator.Send(new DeleteBrandCommand(id));
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    throw new Exception("Sucedio un error al intentar eliminar marca");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(brandResponse);                
            }
        }
    }
}
