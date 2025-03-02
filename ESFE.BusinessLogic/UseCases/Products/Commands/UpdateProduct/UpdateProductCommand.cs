using ESFE.BusinessLogic.DTOs;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Products.Commands.UpdateProduct;

public record UpdateProductCommand(UpdateProductRequest Request) : IRequest<long>;