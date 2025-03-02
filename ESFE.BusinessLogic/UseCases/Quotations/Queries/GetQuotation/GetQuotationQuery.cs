using ESFE.BusinessLogic.DTOs;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Quotations.Queries.GetQuotation;

public record GetQuotationQuery(long quotationId) : IRequest<QuotationResponse>;