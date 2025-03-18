using ESFE.BusinessLogic.DTOs;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Users.Queries.GetUsers;

public record GetUsersQuery() : IRequest<List<UserResponse>>;
