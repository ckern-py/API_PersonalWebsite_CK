using API_Metadata.Models_API;
using API_Metadata.Models_DB;
using Data;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace PersonalWebsite_API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PageVisitController : Controller
    {
        private readonly ILogger<PageVisitController> _logger;
        private readonly IAzureDB _azureDB;

        public PageVisitController(ILogger<PageVisitController> logger, IAzureDB azuredb)
        {
            _logger = logger;
            _azureDB = azuredb;
        }

        [HttpPost]
        public JsonResult InsertPageVisit(InsertPageVisitRequest pageRequest)
        {
            DateTime startDT = DateTime.UtcNow;
            string errorMessage = string.Empty;
            BaseResponse response = new();
            try
            {
                _logger.LogInformation("Begin InsertPageVisit");
                int inserted = _azureDB.InsertPageVisit(pageRequest.PageName);

                if (inserted > 0)
                {
                    response.Status = APIConstants.ResponseMessages.Success;
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                    _logger.LogInformation("End InsertPageVisit");
                }
                else
                {
                    _logger.LogInformation("Page Insert Failed");
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.Status = APIConstants.ResponseMessages.Failure;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                _logger.LogError(ex, "Error in InsertPageVisit");
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Status = APIConstants.ResponseMessages.Failure;
            }
            finally
            {
                ApiLogging logRequest = Utility.BasicLogRequest();
                logRequest.RequestingSystem = pageRequest.RequestingSystem;
                logRequest.ApiMethod = System.Reflection.MethodBase.GetCurrentMethod().Name;
                logRequest.RequestingStartDt = startDT;
                logRequest.ErrorMessage = errorMessage;
                logRequest.RequestMessage = pageRequest.ToString();
                logRequest.ReturnCode = HttpContext.Response.StatusCode.ToString();
                _azureDB.InsertAPILog(logRequest);
            }
            return new JsonResult(response);
        }
    }
}