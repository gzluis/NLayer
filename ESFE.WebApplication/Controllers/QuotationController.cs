using ESFE.BusinessLogic.DTOs;
using ESFE.BusinessLogic.UseCases.Products.Queries.GetProduct;
using ESFE.BusinessLogic.UseCases.Products.Queries.GetProducts;
using ESFE.BusinessLogic.UseCases.Quotations.Commands.CreateQuotation;
using ESFE.BusinessLogic.UseCases.Quotations.Queries.GetQuotation;
using ESFE.BusinessLogic.UseCases.Quotations.Queries.GetQuotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
            var quotations = await _mediator.Send(new GetQuotationsQuery());
            return View(quotations);
        }


        public async Task<IActionResult> Create()
        {
            var productsData = await _mediator.Send(new GetProductsQuery());

            var products = productsData.Select(s => new
            {
                s.ProductId,
                ProductName = s.ProductCode + " " + s.ProductName + " $" + s.PriceUnitSale
            }).ToList();
            products.Add(new { ProductId = 0L, ProductName = "Seleccionar" });
            ViewData["ProductId"] = new SelectList(products, "ProductId", "ProductName",0);
            return View(new CreateQuotationRequest
            {
                QuotationRegistration=DateTime.Now,
                QuotationDetails = new List<CreateQuotationDetailRequest>()
            });
        }

        // POST: BrandController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateQuotationRequest createQuotationRequest)
        {
            try
            {
                var result = await _mediator.Send(new CreateQuotationCommand(createQuotationRequest));
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    throw new Exception("Sucedio un error al intentar guardar el nuevo producto");
            }
            catch (Exception ex)
            {
                var productsData = await _mediator.Send(new GetProductsQuery());

                var products = productsData.Select(s => new
                {
                    s.ProductId,
                    ProductName = s.ProductCode + " " + s.ProductName + " $" + s.PriceUnitSale
                }).ToList();
                products.Add(new { ProductId = 0L, ProductName = "Seleccionar" });
                ViewData["ProductId"] = new SelectList(products, "ProductId", "ProductName", 0);
                ModelState.AddModelError("", ex.Message);
                return View(createQuotationRequest);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddItemDet(CreateQuotationRequest createQuotationRequest, CreateQuotationDetailRequest det)
        {
            if (createQuotationRequest.QuotationDetails == null)
                createQuotationRequest.QuotationDetails = new List<CreateQuotationDetailRequest>();
            var product = await _mediator.Send(new GetProductQuery(det.ProductId.Value));
            var subTotal = det.Quantity * product.PriceUnitSale;
            if (det.Discount == null)
                det.Discount = 0m;
            if (det.Discount > subTotal)
            {
                det.Discount = subTotal;
                subTotal = 0;
            }
            else
                subTotal -= det.Discount.Value;                         
            createQuotationRequest.QuotationDetails.Add(new CreateQuotationDetailRequest
            {
                ProductId = det.ProductId,
                Discount = det.Discount,
                Quantity = det.Quantity,
                Product = product,
                QuotationDetailId = det.QuotationDetailId,
                QuotationId = det.QuotationId,
                Subtotal = subTotal,
            });
            return PartialView("_QuotationDetail", createQuotationRequest.QuotationDetails);
        }
        public async Task<IActionResult> Detail(long id)
        {
            var quotation = await _mediator.Send(new GetQuotationQuery(id));
            return View(quotation);
        }
    }
}
