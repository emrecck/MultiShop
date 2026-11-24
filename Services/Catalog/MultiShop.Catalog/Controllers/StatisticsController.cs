using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Services.StatisticServices;

namespace MultiShop.Catalog.Controllers;

[Authorize]
[ApiController]
[Route("api/statistics")]
public sealed class StatisticsController(IStatisticService statisticService) : ControllerBase
{
    [HttpGet("GetBrandCount")]
    public async Task<ActionResult<long>> GetBrandCount(CancellationToken cancellationToken)
        => Ok(await statisticService.GetBrandCountAsync(cancellationToken));

    [HttpGet("GetCategoryCount")]
    public async Task<ActionResult<long>> GetCategoryCount(CancellationToken cancellationToken)
        => Ok(await statisticService.GetCategoryCountAsync(cancellationToken));

    [HttpGet("GetProductCount")]
    public async Task<ActionResult<long>> GetProductCount(CancellationToken cancellationToken)
        => Ok(await statisticService.GetProductCountAsync(cancellationToken));

    [HttpGet("GetProductAvgPrice")]
    public async Task<ActionResult<decimal>> GetProductAvgPrice(CancellationToken cancellationToken)
        => Ok(await statisticService.GetProductAvgPriceAsync(cancellationToken));

    [HttpGet("GetMaxPriceProductName")]
    public async Task<ActionResult<string>> GetMaxPriceProductName(CancellationToken cancellationToken)
        => Ok(await statisticService.GetMaxPriceProductNameAsync(cancellationToken));

    [HttpGet("GetMinPriceProductName")]
    public async Task<ActionResult<string>> GetMinPriceProductName(CancellationToken cancellationToken)
        => Ok(await statisticService.GetMinPriceProductNameAsync(cancellationToken));
}
