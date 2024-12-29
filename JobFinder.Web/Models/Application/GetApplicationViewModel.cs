using JobFinder.Core.Enums;

namespace JobFinder.Web.Models.Application
{
    public class GetApplicationViewModel: BaseViewModel
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int JobId { get; set; }
        public string CompanyName { get; set; }
        public string JobName { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] FileContent { get; set; }
        public ApplicationJobStates State { get; set; }

    }
}
