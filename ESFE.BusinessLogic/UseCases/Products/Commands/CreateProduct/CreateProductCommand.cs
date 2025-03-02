using ESFE.BusinessLogic.DTOs;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Products.Commands.CreateProduct;

public record CreateProductCommand(CreateProductRequest Request) : IRequest<long>;