using Ardalis.Specification;
using ESFE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESFE.BusinessLogic.UseCases.Products.Specifications
{
    public class GetLastProductCodeSpec : Specification<Product>
    {
        public GetLastProductCodeSpec(string prefix) {
            Query.Where(p => p.ProductCode != null && p.ProductCode.StartsWith(prefix))
            .OrderByDescending(p => p.ProductCode);
        }
    }
}
