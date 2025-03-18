using ESFE.DataAccess.Interfaces;
using ESFE.Entities;
using Mapster;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Brands.Commands.CreateBrand;
internal sealed class CreateBrandHandler(IEfRepository<Brand> _repository)
    : IRequestHandler<CreateBrandCommand, int>
{
    public async Task<int> Handle(CreateBrandCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var newBrand = command.Request.Adapt<Brand>();

            var createdBrand = await _repository.AddAsync(newBrand, cancellationToken);
            
            return createdBrand.BrandId;
        }
        catch (Exception)
        {
            return 0;
            throw;
        }
    }
}