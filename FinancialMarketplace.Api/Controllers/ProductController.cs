
using System.ComponentModel.DataAnnotations;

using FinancialMarketplace.Api.Dtos.Account;
using FinancialMarketplace.Api.Dtos.Product;
using FinancialMarketplace.Application.Database.Repositories;
using FinancialMarketplace.Application.Services;

using MapsterMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialMarketplace.Api.Controllers;

[Route("products")]
public class ProductController(IProductService productService, IMapper mapper) : ApiController
{
    private readonly IProductService _productService = productService;
    private readonly IMapper _mapper = mapper;

    [HttpPost()]
    [Authorize(Roles = "user")]
    [Produces("application/json")]
    [ProducesResponseType<BasicProductDto>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Add(CreateProductRequestDto createProductRequestDto)
    {
        var product = await _productService.Add(createProductRequestDto);

        return product.Match(
            result => Created("", _mapper.Map<BasicProductDto>(result)),
            Problem
        );
    }

    [HttpGet()]
    [Authorize(Roles = "user")]
    [Produces("application/json")]
    [ProducesResponseType<GetManyProductsResponseDto>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMany(
        [Required] int page,
        [FromQuery(Name = "page_size")] int? pageSize,
        GetProductsQueryOptionsDto optionsDto)
    {
        var options = _mapper.Map<ProductQueryOptions>(optionsDto);

        var products = await _productService.GetMany(page, pageSize ?? 50, options);

        return products.Match(
            result => Ok(_mapper.Map<GetManyProductsResponseDto>(result)),
            Problem
        );
    }

    [HttpPatch("{id:guid}")]
    [Authorize(Roles = "user")]
    [Produces("application/json")]
    [ProducesResponseType<bool>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(Guid id, UpdateProductRequestDto updateProductRequest)
    {
        var updatedProduct = await _productService.Update(id, updateProductRequest);

        return updatedProduct.Match(
            result => Ok(_mapper.Map<bool>(result)),
            Problem
        );
    }
}
