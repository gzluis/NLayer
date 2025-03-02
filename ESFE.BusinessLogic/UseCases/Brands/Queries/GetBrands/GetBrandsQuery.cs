using ESFE.BusinessLogic.DTOs;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Brands.Queries.GetBrands;

public record GetBrandsQuery() : IRequest<List<BrandResponse>>;

