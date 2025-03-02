using ESFE.BusinessLogic.DTOs;
using ESFE.DataAccess.Interfaces;
using ESFE.Entities;
using Mapster;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Brands.Queries.GetBrands;

internal sealed class GetBrandsHandler(IEfRepository<Brand> _repository) : IRequestHandler<GetBrandsQuery, List<BrandResponse>>
{
    public async Task<List<BrandResponse>> Handle(GetBrandsQuery query, CancellationToken cancellationToken)
    {
        var brands = await _repository.ListAsync(cancellationToken);

        if (brands == null || !brands.Any())
        {
            return new List<BrandResponse>();
        }

        return brands.Adapt<List<BrandResponse>>();
    }
}
