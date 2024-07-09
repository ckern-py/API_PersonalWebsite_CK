using API_Metadata.Models_DB;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class Utility
    {
        public static string GetSqlConnection(IConfiguration configuration)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                ["Data Source"] = configuration["SQL_SERVER"],
                ["Initial Catalog"] = configuration["SQL_DATABASE"],
                ["User ID"] = configuration["USER_ID"],
                ["Password"] = configuration["USER_PASSWORD"]
            };

            return builder.ToString();
        }

        public static ApiLogging BasicLogRequest()
        {
            return new ApiLogging
            {
                ApplicationName = "API_PersonalWebsite_CK",
                RequestingEndDt = DateTime.UtcNow
            };
        }
    }
}
