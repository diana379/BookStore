using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecParams productParams)
            : base(x =>
                (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains
                (productParams.Search)) &&
                (!productParams.CategoryId.HasValue || x.CategoryId == productParams.CategoryId) &&
                (!productParams.AuthorId.HasValue || x.AuthorId == productParams.AuthorId)
            )
        {

        }
    }
}