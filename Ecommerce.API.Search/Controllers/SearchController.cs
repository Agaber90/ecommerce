using Ecommerce.API.Search.Interfaces;
using Ecommerce.API.Search.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Search.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpPost("search")]
    public async Task<IActionResult> SearchAsync(SearchTerm term)
    {
        var result = await _searchService.SearchAsync(term.CustomerId);

        if (result.IsSuccess)
        {
            return Ok(result.SearchResults);
        }
        return NotFound();
    }
}
