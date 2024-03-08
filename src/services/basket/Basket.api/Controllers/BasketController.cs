using Basket.Application.Commands;
using Basket.Application.GrpcServices;
using Basket.Application.Queries;
using Basket.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

public class BasketController: BaseApiController
{
    private readonly IMediator _mediator;
    private readonly DiscountService _discountService;

    public BasketController(IMediator mediator, DiscountService discountService)
    {
        _mediator = mediator;
        _discountService = discountService;
    }
    
    [HttpGet("[action]/{userName:string}", Name = "GetBasket")]
    public async Task<ActionResult<ShoppingCartResponse>> GetBasket(string userName)
    {
        var query = new GetBasketByUsernameQuery(userName);
        var basket = await _mediator.Send(query);
        return Ok(basket);
    }
    
    [HttpPost("UpdateBasket")]
    [ProducesResponseType(typeof(ShoppingCartResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<ShoppingCartResponse>> UpdateBasket([FromBody] CreateShoppingCartCommand command)
    {
        foreach (var cartItem in command.Items)
        {
            var coupon = await _discountService.GetDiscount(cartItem.ProductName);
            cartItem.Price -= coupon.Amount;
        }
        var basket = await _mediator.Send(command);
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