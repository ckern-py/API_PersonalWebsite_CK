using API_Metadata.Models_API;
using API_Metadata.Models_DB;
using Data;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace PersonalWebsite_API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
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
            DateTime startDT = DateTime.UtcNow;
            string errorMessage = string.Empty;
            GitHubProjectsResponse response = new();

            try
            {
                _logger.LogInformation("Begin GetGitHubProjects");
                response.GitHubProjects = _azureDB.GetGitHubProjects();

                response.Status = APIConstants.ResponseMessages.Success;
                HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                _logger.LogInformation("End GetGitHubProjects");

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                _logger.LogError(ex, "Error in GetGitHubProjects");
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Status = APIConstants.ResponseMessages.Failure;
            }
            finally
            {
                ApiLogging logRequest = Utility.BasicLogRequest();
                logRequest.RequestingSystem = projectRequest.RequestingSystem;
                logRequest.ApiMethod = System.Reflection.MethodBase.GetCurrentMethod().Name;
                logRequest.RequestingStartDt = startDT;
                logRequest.ErrorMessage = errorMessage;
                logRequest.RequestMessage = JsonConvert.SerializeObject(projectRequest);
                logRequest.ResponseMessage = JsonConvert.SerializeObject(response);
                logRequest.ReturnCode = HttpContext.Response.StatusCode.ToString();
                Task.Run(() => _azureDB.InsertAPILog(logRequest));
            }

            return new JsonResult(response);
        }
    }
}
