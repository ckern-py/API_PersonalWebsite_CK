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
        public JsonResult CheckHealth(BaseRequest request)
        {
            DateTime startDT = DateTime.UtcNow;
            string errorMessage = string.Empty;
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
                errorMessage = ex.Message;
                _logger.LogError(ex, "Error in CheckHealth");
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Status = APIConstants.ResponseMessages.Failure;
            }
            finally
            {
                try
                {
                    ApiLogging logRequest = Utility.BasicLogRequest();
                    logRequest.RequestingSystem = request.RequestingSystem;
                    logRequest.ApiMethod = System.Reflection.MethodBase.GetCurrentMethod().Name;
                    logRequest.RequestingStartDt = startDT;
                    logRequest.ErrorMessage = errorMessage;
                    logRequest.RequestMessage = JsonConvert.SerializeObject(request);
                    logRequest.ResponseMessage = JsonConvert.SerializeObject(response);
                    logRequest.ReturnCode = HttpContext.Response.StatusCode.ToString();
                    _azureDB.InsertAPILog(logRequest);
                }
                catch (Exception)
                {
                    //catch so that failed logging doesn't change request response
                }
            }

            return new JsonResult(response);
        }

        [HttpGet]
        public JsonResult CheckHealthDB(BaseRequest request)
        {
            DateTime startDT = DateTime.UtcNow;
            string errorMessage = string.Empty;
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
                errorMessage = ex.Message;
                _logger.LogError(ex, "Error in CheckHealthDB");
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Status = APIConstants.ResponseMessages.Failure;
            }
            finally
            {
                try
                {
                    ApiLogging logRequest = Utility.BasicLogRequest();
                    logRequest.RequestingSystem = request.RequestingSystem;
                    logRequest.ApiMethod = System.Reflection.MethodBase.GetCurrentMethod().Name;
                    logRequest.RequestingStartDt = startDT;
                    logRequest.ErrorMessage = errorMessage;
                    logRequest.RequestMessage = JsonConvert.SerializeObject(request);
                    logRequest.ResponseMessage = JsonConvert.SerializeObject(response);
                    logRequest.ReturnCode = HttpContext.Response.StatusCode.ToString();
                    _azureDB.InsertAPILog(logRequest);
                }
                catch (Exception)
                {
                    //catch so that failed logging doesn't change request response
                }
            }

            return new JsonResult(response);
        }
    }
}
