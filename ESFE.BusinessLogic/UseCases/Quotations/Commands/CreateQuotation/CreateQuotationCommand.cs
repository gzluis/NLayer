using ESFE.BusinessLogic.DTOs;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Quotations.Commands.CreateQuotation;

public record CreateQuotationCommand(CreateQuotationRequest Request) : IRequest<long>;