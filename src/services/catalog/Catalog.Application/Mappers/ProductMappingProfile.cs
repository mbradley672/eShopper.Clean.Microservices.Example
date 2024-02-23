using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Specifications;

namespace Catalog.Application.Mappers;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<ProductBrand, BrandResponse>().ReverseMap();
        CreateMap<ProductType, TypeResponse>().ReverseMap();
        CreateMap<Product, ProductResponse>().ReverseMap();
        CreateMap<Product, CreateProductCommand>().ReverseMap();
        CreateMap<Pagination<Product>, Pagination<ProductResponse>>().ReverseMap();
    }
}

public static class MapperExtensions
{
    private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.ShouldMapProperty = info => info.GetMethod!.IsPublic || info.GetMethod.IsAssembly;
            cfg.AddProfile<ProductMappingProfile>();
        });
        var mapper = config.CreateMapper();
        return mapper;
    });
    
    public static IMapper Mapper => Lazy.Value;
}