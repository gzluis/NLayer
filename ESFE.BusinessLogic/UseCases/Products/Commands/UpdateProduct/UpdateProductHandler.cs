using ESFE.BusinessLogic.UseCases.Brands.Commands.UpdateBrand;
using ESFE.DataAccess.Interfaces;
using ESFE.Entities;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESFE.BusinessLogic.UseCases.Products.Commands.UpdateProduct;

internal sealed class UpdateProductHandler(IEfRepository<Product> _repository) : IRequestHandler<UpdateProductCommand, long>
{
    public async Task<long> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var existingBrand = await _repository.GetByIdAsync(command.Request.ProductId, cancellationToken);
            if (existingBrand is null) return 0;

            existingBrand = command.Request.Adapt(existingBrand);
            await _repository.UpdateAsync(existingBrand, cancellationToken);

            return existingBrand.ProductId;
        }
        catch (Exception)
        {
            return 0;
            throw;
        }
    }
}