namespace JobFinder.Web.Models
{
    public abstract class BaseViewModel
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
    }
}
