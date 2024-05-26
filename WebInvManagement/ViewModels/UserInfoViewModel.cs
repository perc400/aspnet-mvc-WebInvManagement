using Microsoft.AspNetCore.Identity;

namespace WebInvManagement.ViewModels
{
    public class UserInfoViewModel
    {
        public string UserId { set; get; }
        public string UserEmail { set; get; }
        public IList<string> Roles { set; get; }
    }
}
