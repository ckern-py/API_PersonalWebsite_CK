using API_Metadata.Models_API;
using API_Metadata.Models_DB;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class AzureDB : IAzureDB
    {
        private readonly WebsiteDB_Context _context;

        public AzureDB(DbContextOptions<WebsiteDB_Context> option)
        {
            _context = new WebsiteDB_Context(option);
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
