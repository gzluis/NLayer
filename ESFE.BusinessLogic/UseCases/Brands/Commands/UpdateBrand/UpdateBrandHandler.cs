using ESFE.DataAccess.Interfaces;
using ESFE.Entities;
using Mapster;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Brands.Commands.UpdateBrand;

internal sealed class UpdateBrandHandler(IEfRepository<Brand> _repository) : IRequestHandler<UpdateBrandCommand, int>
{
    public async Task<int> Handle(UpdateBrandCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var existingBrand = await _repository.GetByIdAsync(command.Request.BrandId, cancellationToken);
            if (existingBrand is null) return 0;

            existingBrand = command.Request.Adapt(existingBrand);
            await _repository.UpdateAsync(existingBrand, cancellationToken);

            return existingBrand.BrandId;
        }
        catch (Exception)
        {
            return 0;
            throw;
        }
    }
}