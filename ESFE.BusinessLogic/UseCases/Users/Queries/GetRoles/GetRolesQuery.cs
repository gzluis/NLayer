using ESFE.BusinessLogic.DTOs;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Users.Queries.GetRoles;

public record GetRolesQuery : IRequest<List<RoleResponse>>;