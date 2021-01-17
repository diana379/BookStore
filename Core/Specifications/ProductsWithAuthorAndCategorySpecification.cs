using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithAuthorAndCategorySpecification : BaseSpecification<Product>
    {
        public ProductsWithAuthorAndCategorySpecification(ProductSpecParams productParams)
            : base(x =>
                (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains
                (productParams.Search)) &&
                (!productParams.CategoryId.HasValue || x.CategoryId == productParams.CategoryId) &&
                (!productParams.AuthorId.HasValue || x.AuthorId == productParams.AuthorId)
            )
        {
            AddInclude(x => x.Author);
            AddInclude(x => x.Category);
            AddOrderBy(x => x.Name);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1),
            productParams.PageSize);

            if(!string.IsNullOrEmpty(productParams.Sort))
            {
                switch(productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
        }

        public ProductsWithAuthorAndCategorySpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Author);
            AddInclude(x => x.Category);
        }
    }
}