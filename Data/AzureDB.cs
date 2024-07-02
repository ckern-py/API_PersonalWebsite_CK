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
    }
}
