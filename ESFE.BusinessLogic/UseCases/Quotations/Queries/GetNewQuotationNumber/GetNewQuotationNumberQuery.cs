using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESFE.BusinessLogic.UseCases.Quotations.Queries.GetNewQuotationNumber;

public record GetNewQuotationNumberQuery : IRequest<long>;