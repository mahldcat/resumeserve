using Microsoft.AspNetCore.Mvc;

namespace ResumeData.Controllers;

[Route("/v1/[controller]")]
[ApiController]
public class PaginatedData : Controller
{
    [Route("test")]
    [HttpGet]
    public string Test()
    {
        return "Sample Test";
    }
    
    
}