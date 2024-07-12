using API_Metadata.Models_API;
using API_Metadata.Models_DB;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class AzureDB : IAzureDB
    {
        protected readonly WebsiteDB_Context _context;

        public AzureDB(WebsiteDB_Context context)
        {
            _context = context;
        }

        public void CheckDBHealth()
        {
            _context.Database.OpenConnection();
            _context.Database.CloseConnection();
        }

        public int InsertPageVisit(string pageName)
        {
            PageVisit page = new PageVisit()
            {
                PageName = pageName,
                VisitDt = DateTime.UtcNow
            };

            _context.Add(page);
            return _context.SaveChanges();
        }

        public void InsertAPILog(ApiLogging logReq)
        {
            _context.Add(logReq);
            _context.SaveChanges();
        }

        public List<GitHubProjects> GetGitHubProjects()
        {
            List<GitHubProjects> projectList = new List<GitHubProjects>();
            
            projectList = _context.GitHubs
                .Where(p => p.DisplayOnline == true)
                .OrderBy(a => a.DisplayOrder)
                .ThenBy(b => b.GitHubId)
                .Select(g => new GitHubProjects
            {
                ImageSource = g.ImageSource,
                ImageAlt = g.ImageAlt,
                SectionTitle = g.SectionTitle,
                SectionDescription = g.SectionDescription,
                RepoLink = g.RepoLink
            }).ToList();

            return projectList;
        }
    }
}
