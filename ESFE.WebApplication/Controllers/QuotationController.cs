using ESFE.BusinessLogic.DTOs;
using ESFE.BusinessLogic.UseCases.Products.Queries.GetProduct;
using ESFE.BusinessLogic.UseCases.Products.Queries.GetProducts;
using ESFE.BusinessLogic.UseCases.Quotations.Commands.CreateQuotation;
using ESFE.BusinessLogic.UseCases.Quotations.Queries.GetQuotations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ESFE.WebApplication.Controllers
{
    [Authorize]
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

            int userId = int.Parse(User.FindFirst("Id").Value);
            return View(new CreateQuotationRequest
            {
                QuotationRegistration = DateTime.Now,
                UserId = userId,
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
                if(createQuotationRequest.QuotationDetails==null || createQuotationRequest.QuotationDetails.Count==0)
                    throw new Exception("En la cotización es necesario agregar item al detalle");
                createQuotationRequest.QuotationDetails.ForEach(s => s.QuotationDetailId = 0);
                var result = await _mediator.Send(new CreateQuotationCommand(createQuotationRequest));
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    throw new Exception("Sucedio un error al intentar guardar la nueva cotización");
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
        [HttpPost]
        public IActionResult DeleteItemDet(long id,CreateQuotationRequest createQuotationRequest)
        {
            if (createQuotationRequest.QuotationDetails != null && createQuotationRequest.QuotationDetails.Count > 0)
            {
                var delItem = createQuotationRequest.QuotationDetails.FirstOrDefault(s => s.QuotationDetailId == id);
                if (delItem != null)
                {
                    createQuotationRequest.QuotationDetails.Remove(delItem);
                }
            }                   
            return PartialView("_QuotationDetail", createQuotationRequest.QuotationDetails);
        }
    }
}
