using ESFE.BusinessLogic.DTOs;
using ESFE.BusinessLogic.UseCases.Brands.Queries.GetBrands;
using ESFE.BusinessLogic.UseCases.Products.Commands.CreateProduct;
using ESFE.BusinessLogic.UseCases.Products.Commands.UpdateProduct;
using ESFE.BusinessLogic.UseCases.Products.Queries.GetProduct;
using ESFE.BusinessLogic.UseCases.Products.Queries.GetProducts;
using ESFE.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Model.Strings;
using Microsoft.EntityFrameworkCore;

namespace ESFE.WebApplication.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IMediator mediator, IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment;
        }
        // GET: ProductController
        public async Task<IActionResult> Index()
        {
            var products = await _mediator.Send(new GetProductsQuery());
            return View(products);
        }

        // GET: BrandController/Create
        public async Task<IActionResult> Create()
        {
            var brands = await _mediator.Send(new GetBrandsQuery());
            ViewData["BrandId"] = new SelectList(brands, "BrandId", "BrandName");
            return View();
        }
        public async Task<string> SaveImage(IFormFile? file,string url="") {
            string urlImage = url;
            if (file != null && file.Length > 0)
            {
                // Construir la ruta del archivo
                string nameFile = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", nameFile);

                // Guardar la imagen en wwwroot
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Guardar la ruta en la base de datos
                urlImage = "/images/" + nameFile;                
            }
            return urlImage;
        }
        // POST: BrandController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductRequest createProductRequest, IFormFile? file=null)
        {
            try
            {
                createProductRequest.ProductImage = await SaveImage(file);
                var result = await _mediator.Send(new CreateProductCommand(createProductRequest));
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    throw new Exception("Sucedio un error la intentar guardar la nuevo producto");
            }
            catch (Exception ex)
            {
                var brands = await _mediator.Send(new GetBrandsQuery());
                ViewData["BrandId"] = new SelectList(brands, "BrandId", "BrandName");
                ModelState.AddModelError("", ex.Message);
                return View(createProductRequest);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _mediator.Send(new GetProductQuery(id));
            var brands = await _mediator.Send(new GetBrandsQuery());
            ViewData["BrandId"] = new SelectList(brands, "BrandId", "BrandName", product.BrandId);           
            return View(product.Adapt(new UpdateProductRequest()));
        }

        // POST: BrandController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateProductRequest updateProductRequest, IFormFile? file = null)
        {
            try
            {
                updateProductRequest.ProductImage = await SaveImage(file, updateProductRequest.ProductImage);
                var result = await _mediator.Send(new UpdateProductCommand(updateProductRequest));
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    throw new Exception("Sucedio un error la intentar editar producto");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                var brands = await _mediator.Send(new GetBrandsQuery());
                ViewData["BrandId"] = new SelectList(brands, "BrandId", "BrandName", updateProductRequest.BrandId);
                return View(updateProductRequest);
            }
        }             
    }
}
