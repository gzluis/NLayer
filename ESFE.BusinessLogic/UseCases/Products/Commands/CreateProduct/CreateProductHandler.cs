using ESFE.BusinessLogic.UseCases.Products.Specifications;
using ESFE.DataAccess.Interfaces;
using ESFE.Entities;
using Mapster;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESFE.BusinessLogic.UseCases.Products.Commands.CreateProduct;

internal sealed class CreateProductHandler(IEfRepository<Product> _repository)
    : IRequestHandler<CreateProductCommand, long>
{
    public async Task<long> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
		try
		{
			var words = command.Request.ProductCode.Split(' ');
			var prefix = string.Concat(words[0][0], words.Length > 1 ? words[1][0] : 'X' ).ToUpper();

			var lastProduct = await _repository.FirstOrDefaultAsync(new GetLastProductCodeSpec(prefix));

			int newNumber = 1;

			if (lastProduct != null && !string.IsNullOrEmpty(lastProduct.ProductCode)) {
				var numberPart = lastProduct.ProductCode.Substring(prefix.Length); //PD0005
				if (int.TryParse(numberPart, out int lastNumber)) {
					newNumber = lastNumber + 1;
                }
            }

			command.Request.ProductCode = $"{prefix}{newNumber}:D4"; // TX0002

            var newProduct = command.Request.Adapt<Product>();
			var createdProduct = await _repository.AddAsync(newProduct, cancellationToken);
			return createdProduct.ProductId;
		}
		catch (Exception)
		{
			return 0;
            throw;
		}
    }
}