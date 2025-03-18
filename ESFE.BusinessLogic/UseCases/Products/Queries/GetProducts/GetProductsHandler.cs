using ESFE.BusinessLogic.DTOs;
using ESFE.BusinessLogic.UseCases.Products.Specifications;
using ESFE.DataAccess.Interfaces;
using ESFE.Entities;
using Mapster;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Products.Queries.GetProducts
{
    internal sealed class GetProductsHandler(IEfRepository<Product> _repository) : IRequestHandler<GetProductsQuery, List<ProductResponse>>
    {
        public async Task<List<ProductResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.ListAsync(new GetProductWithBrandSpec(), cancellationToken);

            if (products == null && !products.Any()) {
                return new List<ProductResponse>();
            }

            return products.Adapt<List<ProductResponse>>();
        }
    }
}
