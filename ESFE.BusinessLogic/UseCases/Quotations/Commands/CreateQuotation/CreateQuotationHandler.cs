using ESFE.DataAccess.Interfaces;
using ESFE.Entities;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESFE.BusinessLogic.UseCases.Quotations.Commands.CreateQuotation
{
    internal sealed class CreateQuotationHandler(IEfRepository<Quotation> _repository) : IRequestHandler<CreateQuotationCommand, long>
    {
        public async Task<long> Handle(CreateQuotationCommand command, CancellationToken cancellationToken)
        {
			try
			{
				await _repository.BeginTransactionAsync();

				var newQuotation = command.Request.Adapt<Quotation>();

				var createdQuotation = await _repository.AddAsync(newQuotation, cancellationToken);

				await _repository.CommitAsync();

				return createdQuotation.QuotationId;
            }
			catch (Exception)
			{
				await _repository.RollbackAsync();
				throw;
			}
        }
    }
}
