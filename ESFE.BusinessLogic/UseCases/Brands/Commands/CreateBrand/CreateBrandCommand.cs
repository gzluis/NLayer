using ESFE.BusinessLogic.DTOs;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Brands.Commands.CreateBrand;

public record CreateBrandCommand(CreateBrandRequest Request) : IRequest<int>;

