using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<Category> _categoryRepo;
        private readonly IGenericRepository<Author> _authorRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepo,
         IGenericRepository<Category> categoryRepo,
          IGenericRepository<Author> authorRepo, IMapper mapper)
        {
            _mapper = mapper;
            _authorRepo = authorRepo;
            _productsRepo = productsRepo;
            _categoryRepo = categoryRepo;
        }

        [HttpGet]

        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithAuthorAndCategorySpecification();

            var products = await _productsRepo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithAuthorAndCategorySpecification(id);

            var product = await _productsRepo.GetEntityWithSpec(spec);

            return _mapper.Map<Product, ProductToReturnDto>(product);

        }

        [HttpGet("author")]

        public async Task<ActionResult<IReadOnlyList<Author>>> GetAuthor()
        {
            return Ok(await _authorRepo.ListAllAsync());
        }

        [HttpGet("category")]

        public async Task<ActionResult<IReadOnlyList<Category>>> GetCategory()
        {
            return Ok(await _categoryRepo.ListAllAsync());
        }
    }
}