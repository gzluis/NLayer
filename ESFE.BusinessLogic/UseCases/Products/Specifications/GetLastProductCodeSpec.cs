using Ardalis.Specification;
using ESFE.Entities;

namespace ESFE.BusinessLogic.UseCases.Products.Specifications
{
    internal class GetLastProductCodeSpec : Specification<Product>
    {
        public GetLastProductCodeSpec(string prefix)
        {
            Query.Where(p => p.ProductCode != null && p.ProductCode.StartsWith(prefix))
            .OrderByDescending(p => p.ProductCode);
        }
    }
}
