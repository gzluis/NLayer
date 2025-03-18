using ESFE.BusinessLogic.DTOs;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Users.Queries.GetUserAuthenticated;

public record GetUserAuthenticatedQuery(string userName, string password)
    : IRequest<UserResponse>;