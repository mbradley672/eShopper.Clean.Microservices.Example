using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[ApiVersion("v1")]
[ApiController,Route("api/v{version:apiVersion}/[controller]")]
public class BaseApiController: ControllerBase
{
    
}

public class CatalogController : BaseApiController
{
    
}