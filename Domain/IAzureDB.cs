namespace Domain
{
    public interface IAzureDB
    {
        public void CheckDBHealth();
        public int InsertPageVisit(string pageName);
    }
}
