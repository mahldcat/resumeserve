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

    [Route("data/{id}")]
    [HttpGet]
    public async Task<PaginatedDataEntry> GetDataEntryById(int id)
    {
        return await PaginatedData.FetchByIdAsync(id);
    }

    [Route("data")]
    [HttpGet]
    public async Task<(IList<PaginatedDataEntry> Entries, int TotalCount)> GetDataEntries(
        [FromQuery] int pageId, 
        [FromQuery] int maxPageSize)
    {
        return await PaginatedData.FetchByIdAsync(pageId, maxPageSize);
    }
    
    
    
    
}