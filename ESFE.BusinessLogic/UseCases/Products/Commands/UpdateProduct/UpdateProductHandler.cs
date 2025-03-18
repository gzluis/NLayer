using ESFE.DataAccess.Interfaces;
using ESFE.Entities;
using Mapster;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Products.Commands.UpdateProduct
{
    internal sealed class UpdateProductHandler(IEfRepository<Product> _repository) : IRequestHandler<UpdateProductCommand, long>
    {
        public async Task<long> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var existingProduct = await _repository.GetByIdAsync(command.Request.ProductId, cancellationToken);
                if (existingProduct is null) return 0;

                existingProduct = command.Request.Adapt(existingProduct);
                await _repository.UpdateAsync(existingProduct, cancellationToken);

                return existingProduct.ProductId;
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
        }
    }
}
