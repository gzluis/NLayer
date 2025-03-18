using ESFE.BusinessLogic.DTOs;
using ESFE.Entities;
using Mapster;

namespace ESFE.BusinessLogic.Mappings
{
    public class MappingRegister : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Product, ProductResponse>()
                .Map(pd => pd.BrandName, p => p.Brand.BrandName);

            config.NewConfig<User, UserResponse>()
                .Map(ud => ud.RolName, u => u.Rol.RolName);
        }
    }
}
