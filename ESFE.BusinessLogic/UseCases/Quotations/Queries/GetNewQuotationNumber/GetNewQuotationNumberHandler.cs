using ESFE.DataAccess.Interfaces;
using ESFE.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESFE.BusinessLogic.UseCases.Quotations.Queries.GetNewQuotationNumber;

internal sealed class GetNewQuotationNumberHandler(IEfRepository<Quotation> _repository)
    : IRequestHandler<GetNewQuotationNumberQuery, long>
{
    public async Task<long> Handle(GetNewQuotationNumberQuery query, CancellationToken cancellationToken)
    {
        var quotations = await _repository.ListAsync(cancellationToken);

        var lastQuotationNumber = quotations.OrderByDescending(q=>q.QuotationNumber)
            .Select(q => q.QuotationNumber)
            .FirstOrDefault();

        return lastQuotationNumber + 1;
    }

}