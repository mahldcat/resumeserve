
using Microsoft.AspNetCore.Mvc;
using ResumeData.Data;

namespace ResumeData.Controllers
{
    [Route("/v1/[controller]")]
    [ApiController]
    public class ResumeController : ControllerBase
    {
        [Route("test")]
        public string Test()
        {
            return "Sample Test";
        }

        [Route("expertise")]
        public BulletPoints? Expertise()
        {
            return BulletPoints.FromDataFile;
        }
    }
}
