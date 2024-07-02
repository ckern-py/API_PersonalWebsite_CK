using Microsoft.AspNetCore.Mvc;

namespace PersonalWebsite_API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PageVisitController : Controller
    {
        private readonly ILogger<PageVisitController> _logger;

        public PageVisitController(ILogger<PageVisitController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "InsertPageVisit")]
        public void InsertPageVisit()
        {

        }
    }
}