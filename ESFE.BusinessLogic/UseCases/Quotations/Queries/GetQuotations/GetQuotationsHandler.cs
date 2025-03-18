using ESFE.BusinessLogic.DTOs;
using ESFE.BusinessLogic.UseCases.Quotations.Specifications;
using ESFE.DataAccess.Interfaces;
using ESFE.Entities;
using Mapster;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Quotations.Queries.GetQuotations;

internal sealed class GetQuotationsHandler(IEfRepository<Quotation> _repository)
     : IRequestHandler<GetQuotationsQuery, List<QuotationResponse>>
{
    public async Task<List<QuotationResponse>> Handle(GetQuotationsQuery query, CancellationToken cancellationToken)
    {
        var quotations = await _repository.ListAsync(new GetQuotationsSpec(), cancellationToken);

        if (quotations == null || !quotations.Any())
        {
            return new List<QuotationResponse>();
        }

        return quotations.Adapt<List<QuotationResponse>>();
    }
}