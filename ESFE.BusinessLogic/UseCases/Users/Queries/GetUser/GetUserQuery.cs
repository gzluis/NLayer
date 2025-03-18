using ESFE.BusinessLogic.DTOs;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Users.Queries.GetUser;

public record GetUserQuery(int userId) : IRequest<UserByIdResponse>;
