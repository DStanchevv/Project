namespace SportWave.ViewModels.MenAndWomenViewModels
{
    public class MenAndWomenViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Color { get; set; } = null!;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string Gender { get; set; } = null!;
    }
}
