namespace SportWave.ViewModels.MenAndWomenViewModels
{
    public class AllProductsViewModel
    {
        public int CategoryId { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; } = new HashSet<CategoryViewModel>();
        public IEnumerable<MenAndWomenViewModel> Products { get; set; } = new HashSet<MenAndWomenViewModel>();
    }
}
