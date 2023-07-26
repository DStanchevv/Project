using Microsoft.Build.Framework;
using SportWave.Data.Models;

namespace SportWave.ViewModels.AdminViewModels
{
    public class MakeUserEmployeeViewModel
    {
        [Required]
        public string Email { get; set; } = null!;

        public IEnumerable<ApplicationUser> AdminEmails { get; set; } = new HashSet<ApplicationUser>();
        public IEnumerable<ApplicationUser> EmployeeEmails { get; set; } = new HashSet<ApplicationUser>();
    }
}
