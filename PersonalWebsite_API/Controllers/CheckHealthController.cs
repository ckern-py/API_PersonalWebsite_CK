using API_Metadata.Models_API;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace PersonalWebsite_API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CheckHealthController : ControllerBase
    {
        private readonly ILogger<CheckHealthController> _logger;
        private readonly IAzureDB _azureDB;

        public CheckHealthController(ILogger<CheckHealthController> logger, IAzureDB azureDB)
        {
            _logger = logger;
            _azureDB = azureDB;
        }

        [HttpGet]
        public JsonResult CheckHealth()
        {
            BaseResponse response = new();
            try
            {
                _logger.LogInformation("Begin CheckHealth");
                response.Status = APIConstants.ResponseMessages.Success;
                HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                _logger.LogInformation("End CheckHealth");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CheckHealth");
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Status = APIConstants.ResponseMessages.Failure;
            }
            return new JsonResult(response);
        }

        [HttpGet]
        public JsonResult CheckHealthDB()
        {
            BaseResponse response = new();
            try
            {
                _logger.LogInformation("Begin CheckHealthDB");
                _azureDB.CheckDBHealth();

                response.Status = APIConstants.ResponseMessages.Success;
                HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                _logger.LogInformation("End CheckHealthDB");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CheckHealthDB");
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Status = APIConstants.ResponseMessages.Failure;
            }
            return new JsonResult(response);
        }
    }
}
