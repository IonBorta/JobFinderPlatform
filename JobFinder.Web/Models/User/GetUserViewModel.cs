using JobFinder.Core.Enums;

namespace JobFinder.Web.Models.User
{
    public class GetUserViewModel: EditUserViewModel
    {
        public UserType UserType { get; set; }
    }
}
