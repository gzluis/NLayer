using MediatR;

namespace ESFE.BusinessLogic.UseCases.Quotations.Queries.GetQuotationNumber;

public record GetQuotationNumberQuery : IRequest<long>;