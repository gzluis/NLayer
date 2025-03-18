using MediatR;

namespace ESFE.BusinessLogic.UseCases.Brands.Commands.DeleteBrand;

public record DeleteBrandCommand(int brandId) : IRequest<int>;