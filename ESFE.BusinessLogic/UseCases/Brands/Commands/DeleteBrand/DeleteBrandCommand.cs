using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESFE.BusinessLogic.UseCases.Brands.Commands.DeleteBrand;

public record DeleteBrandCommand(int brandId) : IRequest<int>;
