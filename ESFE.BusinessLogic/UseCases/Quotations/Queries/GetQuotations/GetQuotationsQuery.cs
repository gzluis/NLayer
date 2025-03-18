using ESFE.BusinessLogic.DTOs;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Quotations.Queries.GetQuotations;

public record GetQuotationsQuery : IRequest<List<QuotationResponse>>;