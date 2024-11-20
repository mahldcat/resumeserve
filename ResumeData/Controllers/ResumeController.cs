
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

        [Route("profile")]
        public Profile? Profile()
        {
            return Data.Profile.FromFile();
        }
        
        [Route("expertise")]
        public Expertise? Expertise()
        {
            return Data.Expertise.FromFile();
        }

        [Route("education")]
        public Education? Education()
        {
            return Data.Education.FromFile();
        }

        [Route("experience")]
        public Experience? Experience()
        {
            return Data.Experience.FromFile();
        }
    }
}
