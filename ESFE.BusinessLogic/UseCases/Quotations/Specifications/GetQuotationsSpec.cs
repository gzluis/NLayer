using Ardalis.Specification;
using ESFE.Entities;

namespace ESFE.BusinessLogic.UseCases.Quotations.Specifications
{
    public class GetQuotationsSpec : Specification<Quotation>
    {
        public GetQuotationsSpec()
        {
            Query.Include(q => q.QuotationDetails);
            Query.Include(q => q.User);
        }
    }
}
