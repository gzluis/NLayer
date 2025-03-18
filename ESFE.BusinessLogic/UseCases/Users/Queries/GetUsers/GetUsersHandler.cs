using ESFE.BusinessLogic.DTOs;
using ESFE.BusinessLogic.UseCases.Users.Specifications;
using ESFE.DataAccess.Interfaces;
using ESFE.Entities;
using Mapster;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Users.Queries.GetUsers;
internal sealed class GetUsersHandler(IEfRepository<User> _repository)
    : IRequestHandler<GetUsersQuery, List<UserResponse>>
{
    public async Task<List<UserResponse>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var users = await _repository.ListAsync(new GetUsersSpec(),cancellationToken);

        if (users == null || !users.Any())
        {
            return new List<UserResponse>();
        }

        return users.Adapt<List<UserResponse>>();
    }
}