using Basket.Application.Commands;
using Basket.Application.Queries;
using Basket.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

public class BasketController: BaseApiController
{
    private readonly IMediator _mediator;

    public BasketController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("[action]/{userName:string}", Name = "GetBasket")]
    public async Task<ActionResult<ShoppingCartResponse>> GetBasket(string userName)
    {
        var query = new GetBasketByUsernameQuery(userName);
        var basket = await _mediator.Send(query);
        return Ok(basket);
    }
    
    [HttpPost("CreateBasket")]
    [ProducesResponseType(typeof(ShoppingCartResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<ShoppingCartResponse>> CreateBasket([FromBody] CreateShoppingCartCommand command)
    {
        var basket = await _mediator.Send(command);
        return Ok(basket);
    }

    [HttpGet("[action]/{userName:string}", Name = "DeleteBasketByUserName")]
    public async Task<ActionResult<ShoppingCartResponse>> DeleteBasket(string userName)
    {
        var query = new DeleteBasketByUserNameQuery(userName);
        return Ok(await _mediator.Send(query));
    }
}