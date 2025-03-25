using Ardalis.Specification;
using ESFE.Entities;

namespace ESFE.BusinessLogic.UseCases.Quotations.Specifications
{
    public class GetQuotationSpec : Specification<Quotation>
    {
        public GetQuotationSpec(long quotationId)
        {
            Query.Where(q => q.QuotationId == quotationId);
            Query.Include(q => q.QuotationDetails);
            Query.Include(q => q.User);
        }
    }
}
