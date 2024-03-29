﻿using AutoMapper;
using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetAllBrandsHandler(IBrandRepository brandRepository)
    : IRequestHandler<GetAllBrandsQuery, IList<BrandResponse>>
{
    public async Task<IList<BrandResponse>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        var brandList = await brandRepository.GetAllBrandsAsync();
        var brandResponseList = MapperExtensions.Mapper.Map<IList<ProductBrand>, IList<BrandResponse>>(brandList.ToList());
        return brandResponseList;
    }
}