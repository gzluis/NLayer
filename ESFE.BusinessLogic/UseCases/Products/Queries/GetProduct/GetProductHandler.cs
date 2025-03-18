using ESFE.BusinessLogic.DTOs;
using ESFE.DataAccess.Interfaces;
using ESFE.Entities;
using Mapster;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Products.Queries.GetProduct
{
    internal sealed class GetProductHandler(IEfRepository<Product> _repository) : IRequestHandler<GetProductQuery, ProductByIdResponse>
    {
        public async Task<ProductByIdResponse> Handle(GetProductQuery query, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(query.productId, cancellationToken);

            if (product is null)
            {
                return new ProductByIdResponse();
            }

            return product.Adapt<ProductByIdResponse>();
        }
    }
}
