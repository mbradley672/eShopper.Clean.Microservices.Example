using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Specifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

public class ProductsController : BaseApiController
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("[action]", Name = "GetAllBrands")]
    [ProducesResponseType(typeof(IList<BrandResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IList<BrandResponse>>> GetAllBrands()
    {
        var query = new GetAllBrandsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("[action]", Name = "GetAllProductTypes")]
    [ProducesResponseType(typeof(IList<BrandResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IList<TypeResponse>>> GetAllProductTypes()
    {
        var query = new GetAllTypesQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("[action]/{id:string}", Name = "GetProductById")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<ProductResponse>> GetProductById(string id)
    {
        var query = new GetProductByIdQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("[action]/{productName:string}", Name = "GetProductByName")]
    [ProducesResponseType(typeof(IList<ProductResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IList<ProductResponse>>> GetProductByName(string productName)
    {
        var query = new GetProductByNameQuery(productName);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("[action]/{brandName:string}", Name = "GetProductByBrandName")]
    [ProducesResponseType(typeof(IList<ProductResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IList<ProductResponse>>> GetProductByBrandName(string brandName)
    {
        var query = new GetAllProductsByBrandQuery(brandName);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("GetAllProducts")]
    [ProducesResponseType(typeof( ProductResponse ), StatusCodes.Status200OK)]
    public async Task<ActionResult<ProductResponse>> GetAllProducts([FromQuery] CatalogSpecificationParams specificationParams)
    {
        var query = new GetAllProductsQuery(specificationParams);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpPost("CreateProduct")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<ProductResponse>> CreateProduct([FromBody] CreateProductCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpPut("UpdateProduct")]
    public async Task<IActionResult> UpdateProduct([FromBody] CreateProductCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetProductById), new { id = result.Id });
    }
    
    [HttpDelete("{id:string}", Name = "DeleteProduct")]
    public async Task<IActionResult> UpdateProduct(string id)
    {
        var query = new DeleteProductByIdQuery(id);
        return Ok(await _mediator.Send(query));
    }
    
}