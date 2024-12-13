
using Microsoft.AspNetCore.Mvc;
using ResumeData.Data;

namespace ResumeData.Controllers
{
    [Route("/v1/[controller]")]
    [ApiController]
    public class ResumeController : ControllerBase
    {
        [Route("test")]
        [HttpGet]
        public string Test()
        {
            return "Sample Test";
        }

        [Route("profile")]
        [HttpGet]
        public Profile? Profile()
        {
            return Data.Profile.FromFile();
        }
        
        [Route("expertise")]
        [HttpGet]
        public Expertise? Expertise()
        {
            return Data.Expertise.FromFile();
        }

        [Route("education")]
        [HttpGet]
        public Education? Education()
        {
            return Data.Education.FromFile();
        }

        [Route("experience")]
        [HttpGet]
        public Experience? Experience()
        {
            return Data.Experience.FromFile();
        }
    }
}
