using System.ComponentModel.DataAnnotations;

namespace SportWave.ViewModels.StaffViewModels
{
    public class AddStoreViewModel
    {
        public int? Id { get; set; }
        
        [Required]
        public string Country { get; set; } = null!;

        [Required]
        public string Region { get; set; } = null!;

        [Required]
        public string City { get; set; } = null!;

        [Required]
        public string Location { get; set; } = null!;
    }
}
