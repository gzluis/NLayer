using ESFE.BusinessLogic.DTOs;
using ESFE.BusinessLogic.UseCases.Brands.Queries.GetBrand;
using ESFE.DataAccess.Interfaces;
using ESFE.Entities;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESFE.BusinessLogic.UseCases.Users.Queries.GetUser;

internal sealed class GetUserHandler(IEfRepository<User> _repository) : IRequestHandler<GetUserQuery, UserResponse>
{
    public async Task<UserResponse> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(query.userId, cancellationToken);

        if (user is null)
        {
            return new UserResponse();
        }

        return user.Adapt<UserResponse>();
    }
}
