using Microsoft.Build.Framework;

namespace SportWave.ViewModels.AdminViewModels
{
    public class MakeUserAdminViewModel
    {
        [Required]
        public string Email { get; set; } = null!;

        public IEnumerable<string> Emails { get; set; } = new HashSet<string>();
    }
}
