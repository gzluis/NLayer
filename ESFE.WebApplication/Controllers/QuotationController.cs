using ESFE.BusinessLogic.DTOs;
using ESFE.BusinessLogic.UseCases.Brands.Queries.GetBrands;
using ESFE.BusinessLogic.UseCases.Products.Commands.CreateProduct;
using ESFE.BusinessLogic.UseCases.Products.Queries.GetProducts;
using ESFE.BusinessLogic.UseCases.Quotations.Commands.CreateQuotation;
using ESFE.BusinessLogic.UseCases.Quotations.Queries.GetQuotation;
using ESFE.BusinessLogic.UseCases.Quotations.Queries.GetQuotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ESFE.WebApplication.Controllers
{
    public class QuotationController : Controller
    {
        private readonly IMediator _mediator;

        public QuotationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: QuotationController
        public async Task<IActionResult> Index()
        {
            var quotations = await _mediator.Send(new GetQuotationQuery(3));
            return View(quotations);
        }


        public async Task<IActionResult> Create()
        {
            var products = await _mediator.Send(new GetProductsQuery());
            ViewData["ProductId"] = new SelectList(products, "ProductId", "ProductName");
            return View();
        }

        // POST: BrandController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateQuotationRequest createProductRequest)
        {
            try
            {
                var result = await _mediator.Send(new CreateQuotationCommand(createProductRequest));
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    throw new Exception("Sucedio un error al intentar guardar el nuevo producto");
            }
            catch (Exception ex)
            {
                var products = await _mediator.Send(new GetProductsQuery());
                ViewData["ProductId"] = new SelectList(products, "ProductId", "ProductName");
                ModelState.AddModelError("", ex.Message);
                return View(createProductRequest);
            }
        }
        
    }
}
