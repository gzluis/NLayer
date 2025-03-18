using Ardalis.Specification;
using ESFE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESFE.BusinessLogic.UseCases.Products.Specifications
{
    public class GetProductWithBrandSpec : Specification<Product>
    {
        public GetProductWithBrandSpec() { 
            Query.Include(p => p.Brand);
        }
    }
}
