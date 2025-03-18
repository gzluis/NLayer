using Ardalis.Specification;
using ESFE.Entities;

namespace ESFE.BusinessLogic.UseCases.Users.Specifications;

public class GetUsersSpec : Specification<User>
{
    public GetUsersSpec()
    {
        Query.Include(u => u.Rol);
    }
}