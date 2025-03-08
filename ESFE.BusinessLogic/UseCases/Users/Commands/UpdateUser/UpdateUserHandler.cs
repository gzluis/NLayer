using ESFE.BusinessLogic.UseCases.Brands.Commands.UpdateBrand;
using ESFE.DataAccess.Interfaces;
using ESFE.Entities;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESFE.BusinessLogic.UseCases.Users.Commands.UpdateUser;

internal sealed class UpdateUserHandler(IEfRepository<User> _repository) : IRequestHandler<UpdateUserCommand, int>
{
    public async Task<int> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var existingUser = await _repository.GetByIdAsync(command.Request.UserId, cancellationToken);
            if (existingUser is null) return 0;

            existingUser = command.Request.Adapt(existingUser);
            await _repository.UpdateAsync(existingUser, cancellationToken);

            return existingUser.UserId;
        }
        catch (Exception)
        {
            return 0;
            throw;
        }
    }
}