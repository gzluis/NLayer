using ESFE.BusinessLogic.DTOs;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Products.Queries.GetProducts;

public record GetProductsQuery() : IRequest<List<ProductResponse>>;
