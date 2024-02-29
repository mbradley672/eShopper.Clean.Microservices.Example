using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[ApiVersion("1")]
[ApiController,Route("api/v{version:apiVersion}/[controller]")]
public class BaseApiController: ControllerBase
{
    
}