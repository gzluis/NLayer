using ESFE.BusinessLogic.DTOs;
using ESFE.BusinessLogic.UseCases.Users.Specifications;
using ESFE.DataAccess.Interfaces;
using ESFE.Entities;
using Mapster;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Users.Queries.GetUserAuthenticated;

internal sealed class GetUserAuthenticatedHandler(IEfRepository<User> _repository) : IRequestHandler<GetUserAuthenticatedQuery, UserResponse>
{
    public async Task<UserResponse> Handle(GetUserAuthenticatedQuery query, CancellationToken cancellationToken)
    {
        var User = await _repository.FirstOrDefaultAsync(new GetUserAuthenticatedSpec(query.userName, query.password), cancellationToken);

        if (User is null)
        {
            return new UserResponse();
        }

        return User.Adapt<UserResponse>();
    }
}