using Microsoft.AspNetCore.Identity;

namespace SensorApi.Models
{
    public class ManageUsersViewModel
    {
        public IdentityUser[] Administrators { get; set; }

        public IdentityUser[] Users { get; set; }
        public IdentityUser[] Devices { get; set; }
    }
}
