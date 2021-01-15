using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithAuthorAndCategorySpecification : BaseSpecification<Product>
    {
        public ProductsWithAuthorAndCategorySpecification()
        {
            AddInclude(x => x.Author);
            AddInclude(x => x.Category);
        }

        public ProductsWithAuthorAndCategorySpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Author);
            AddInclude(x => x.Category);
        }
    }
}