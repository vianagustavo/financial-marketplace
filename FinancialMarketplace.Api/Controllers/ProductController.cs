
using FinancialMarketplace.Api.Dtos.Account;
using FinancialMarketplace.Api.Dtos.Users;
using FinancialMarketplace.Application.Services;

using MapsterMapper;

using Microsoft.AspNetCore.Mvc;

namespace FinancialMarketplace.Api.Controllers;

[Route("products")]
public class ProductController(IProductService productService, IMapper mapper) : ApiController
{
    private readonly IProductService _productService = productService;
    private readonly IMapper _mapper = mapper;

    [HttpPost("funds")]
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
}
