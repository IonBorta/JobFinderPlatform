namespace JobFinder.Web.Models
{
    public class ApplyModalModel
    {
        public int JobId { get; set; }
        public int CompanyId { get; set; }
        public int? ApplicationId { get; set; } // Nullable if it's optional
    }
}
