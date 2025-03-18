using ESFE.BusinessLogic.DTOs;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Users.Commands.UpdateUser;

public record UpdateUserCommand(UpdateUserRequest Request) : IRequest<int>;
