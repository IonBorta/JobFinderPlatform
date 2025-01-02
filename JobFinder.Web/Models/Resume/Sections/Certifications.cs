namespace JobFinder.Web.Models.Resume.Sections
{
    public class Certifications
    {
        public required string CertificationName { get; set; }
        public required DateTime Year { get; set; }
        public required string From { get; set; }
    }
}
