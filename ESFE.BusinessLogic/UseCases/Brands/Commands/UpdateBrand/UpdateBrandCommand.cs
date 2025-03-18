using ESFE.BusinessLogic.DTOs;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Brands.Commands.UpdateBrand;

public record UpdateBrandCommand(UpdateBrandRequest Request) : IRequest<int>;