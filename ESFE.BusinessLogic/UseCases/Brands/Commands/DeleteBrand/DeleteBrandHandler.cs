using ESFE.DataAccess.Interfaces;
using ESFE.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESFE.BusinessLogic.UseCases.Brands.Commands.DeleteBrand;

internal sealed class DeleteBrandHandler(IEfRepository<Brand> _repository)
    : IRequestHandler<DeleteBrandCommand, int>
{
    public async Task<int> Handle(DeleteBrandCommand command, CancellationToken cancellationToken)
    {
        var existingBrand = await _repository.GetByIdAsync(command.brandId);

        if (existingBrand is null) return 0;

        await _repository.DeleteAsync(existingBrand, cancellationToken);

        return existingBrand.BrandId;
    }
}