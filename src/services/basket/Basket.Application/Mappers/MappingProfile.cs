using AutoMapper;
using Basket.Application.Responses;
using Basket.Core.Entities;

namespace Basket.Application.Mappers;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<ShoppingCart, ShoppingCartResponse>().ReverseMap();
        CreateMap<ShoppingCartItem, ShoppingCartItemResponse>().ReverseMap();
    }
}

public static class MapperExtensions
{
    private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.ShouldMapProperty = info => info.GetMethod!.IsPublic || info.GetMethod.IsAssembly;
            cfg.AddProfile<MappingProfile>();
        });
        var mapper = config.CreateMapper();
        return mapper;
    });
    
    public static IMapper Mapper => Lazy.Value;
}
