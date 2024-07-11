using API_Metadata.Models_API;
using API_Metadata.Models_DB;

namespace Domain
{
    public interface IAzureDB
    {
        public void CheckDBHealth();
        public int InsertPageVisit(string pageName);
        public void InsertAPILog(ApiLogging logReq);
        public List<GitHubProjects> GetGitHubProjects();
    }
}
