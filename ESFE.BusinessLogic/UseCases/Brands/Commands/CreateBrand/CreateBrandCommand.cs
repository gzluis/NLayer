using ESFE.BusinessLogic.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESFE.BusinessLogic.UseCases.Brands.Commands.CreateBrand;

public record CreateBrandCommand(CreateBrandRequest Request) : IRequest<int>;