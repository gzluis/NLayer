using ESFE.BusinessLogic.DTOs;
using ESFE.BusinessLogic.UseCases.Brands.Queries.GetBrand;
using ESFE.BusinessLogic.UseCases.Quotations.Queries.GetQuotationNumber;
using ESFE.DataAccess.Interfaces;
using ESFE.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESFE.BusinessLogic.UseCases.Quotations.Queries.GetQuotationNumber;

internal sealed class GetQuotationNumberHandler(IEfRepository<Quotation> _repository) : IRequestHandler<GetQuotationNumberQuery, long>
{
    public async Task<long> Handle(GetQuotationNumberQuery query, CancellationToken cancellationToken)
    {
        var quotations = await _repository.ListAsync(cancellationToken);

        var lastQuotationNumber = quotations.OrderByDescending(q => q.QuotationNumber)
                .Select(q => q.QuotationNumber)
                .FirstOrDefault();

        return lastQuotationNumber + 1; 
    }
}
