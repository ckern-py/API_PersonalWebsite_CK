using API_Metadata.Models_API;
using API_Metadata.Models_DB;
using Data;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace PersonalWebsite_API.Controllers
{
    public class GitHubController : Controller
    {
        private readonly ILogger<GitHubController> _logger;
        private readonly IAzureDB _azureDB;

        public GitHubController(ILogger<GitHubController> logger, IAzureDB azuredb)
        {
            _logger = logger;
            _azureDB = azuredb;
        }

        [HttpPost]
        public JsonResult GetGitHubProjects(BaseRequest projectRequest)
        {
            
        }
    }
}
