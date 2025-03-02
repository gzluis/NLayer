using ESFE.BusinessLogic.DTOs;
using ESFE.BusinessLogic.UseCases.Brands.Queries.GetBrands;
using ESFE.DataAccess.Interfaces;
using ESFE.Entities;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESFE.BusinessLogic.UseCases.Products.Queries.GetProducts;

internal sealed class GetProductsHandler(IEfRepository<Product> _repository) : IRequestHandler<GetProductsQuery, List<ProductResponse>>
{
    public async Task<List<ProductResponse>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await _repository.ListAsync(cancellationToken);

        if (products == null || !products.Any())
        {
            return new List<ProductResponse>();
        }

        return products.Adapt<List<ProductResponse>>();
    }
}
