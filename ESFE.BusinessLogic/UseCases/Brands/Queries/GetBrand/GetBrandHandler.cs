using ESFE.BusinessLogic.DTOs;
using ESFE.DataAccess.Interfaces;
using ESFE.Entities;
using Mapster;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Brands.Queries.GetBrand;

internal sealed class GetBrandHandler(IEfRepository<Brand> _repository) : IRequestHandler<GetBrandQuery, BrandResponse>
{
    public async Task<BrandResponse> Handle(GetBrandQuery query, CancellationToken cancellationToken)
    {
        var brand = await _repository.GetByIdAsync(query.brandId, cancellationToken);

        if (brand is null)
        {
            return new BrandResponse();
        }

        return brand.Adapt<BrandResponse>();
    }
}
