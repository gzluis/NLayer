using ESFE.BusinessLogic.DTOs;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Brands.Queries.GetBrand;

public record GetBrandQuery(int brandId) : IRequest<BrandResponse>;
