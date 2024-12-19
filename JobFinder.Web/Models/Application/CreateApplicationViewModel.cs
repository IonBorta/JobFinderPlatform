namespace JobFinder.Web.Models.Application
{
    public class CreateApplicationViewModel: BaseViewModel
    {
        public CreateApplicationViewModel()
        {
            Created = DateTime.Now;
        }
        public int JobId { get; set; }
        public int UserId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] FileContent { get; set; }
    }
}
