namespace SportWave.ViewModels.AdminViewModels
{
    public class PromoCodesViewModel
    {
        public Guid CodeId { get; set; }
        public string Code { get; set; } = null!;
        public int Value { get; set; }
        public bool IsValid { get; set; }
    }
}
