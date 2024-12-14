namespace JobFinder.Web.Models
{
    public abstract class BaseViewModel
    {
        public BaseViewModel()
        {
            Created = DateTime.UtcNow;
        }
        public DateTime Created { get; set; }
    }
}
