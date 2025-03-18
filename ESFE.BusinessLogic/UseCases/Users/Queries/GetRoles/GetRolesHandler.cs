using ESFE.BusinessLogic.DTOs;
using ESFE.DataAccess.Interfaces;
using ESFE.Entities;
using Mapster;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Users.Queries.GetRoles;

internal sealed class GetRolesHandler(IEfRepository<Role> _repository)
    : IRequestHandler<GetRolesQuery, List<RoleResponse>>
{
    public async Task<List<RoleResponse>> Handle(GetRolesQuery query, CancellationToken cancellationToken)
    {
        var roles = await _repository.ListAsync(cancellationToken);

        if (roles == null || !roles.Any())
        {
            return new List<RoleResponse>();
        }

        return roles.Adapt<List<RoleResponse>>();
    }
}
