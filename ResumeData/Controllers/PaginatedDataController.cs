using DataAccess;
using DataAccess.Poco;
using Microsoft.AspNetCore.Mvc;

namespace ResumeData.Controllers;


/// <summary>
/// Another data driven data source that can be used for testing SPWApps
/// </summary>
[Route("/v1/[controller]")]
[ApiController]
public class PaginatedDataController(IPaginatedData paginatedData) : Controller
{
    private IPaginatedData PaginatedData { get; }  = paginatedData;
    
    [Route("test")]
    [HttpGet]
    public string Test()
    {
        return "Sample Test";
    }

    private async Task<IActionResult> Handler(Func<Task<object?>> fetchData)
    {
        try
        {
            var result = await fetchData(); // Await the asynchronous action
            return Ok(result); // Return the result wrapped in an Ok response
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.ToString());
        }
    }

    [Route("data/{id}")]
    [HttpGet]
    public async Task<IActionResult> GetDataEntryById(int id)
    {
        return await Handler(
        async () =>
        {
            return await PaginatedData.FetchByIdAsync(id);
        });
    }

    [Route("data")]
    [HttpGet]
    public async Task<IActionResult> GetDataEntries(
        [FromQuery] int pageId, 
        [FromQuery] int maxPageSize)
    {
        return await Handler(
        async () =>
        {
            var results = await PaginatedData.FetchByIdAsync(pageId, maxPageSize);
            return new ResultSet
            {
                Entries = results.Entries,
                TotalPages = results.TotalPages
            };
        });
}



    public struct ResultSet
    {
        public IList<PaginatedDataEntry> Entries { get; set; }
        public int TotalPages { get; set; }
    }
}