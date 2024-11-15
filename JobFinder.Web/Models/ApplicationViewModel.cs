namespace JobFinder.Web.Models
{
    public class ApplicationViewModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string UserName {  get; set; }
        public string JobName { get; set; }
        public string UserEmail { get; set; }
        public string FilePath {  get; set; }
        public DateTime Submited { get; set; }
    }
}
