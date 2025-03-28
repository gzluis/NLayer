﻿using ESFE.BusinessLogic.DTOs;
using ESFE.BusinessLogic.UseCases.Quotations.Specifications;
using ESFE.DataAccess.Interfaces;
using ESFE.Entities;
using Mapster;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Quotations.Queries.GetQuotation;

internal sealed class GetQuotationHandler(IEfRepository<Quotation> _repository)
    : IRequestHandler<GetQuotationQuery, QuotationResponse>
{
    public async Task<QuotationResponse> Handle(GetQuotationQuery query, CancellationToken cancellationToken)
    {
        var quotation = await _repository.FirstOrDefaultAsync(new GetQuotationSpec(query.quotationId), cancellationToken);

        if (quotation is null)
        {
            return new QuotationResponse();
        }

        return quotation.Adapt<QuotationResponse>();
    }
}