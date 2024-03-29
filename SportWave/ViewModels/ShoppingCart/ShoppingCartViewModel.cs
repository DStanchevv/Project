﻿namespace SportWave.ViewModels.ShoppingCart
{
    public class ShoppingCartViewModel
    {
        public decimal TotalPrice { get; set; }
        public bool HasPromo { get; set; }

        public IEnumerable<AllProductsInCartViewModel> ProductsInCart { get; set; } = new HashSet<AllProductsInCartViewModel>();
    }
}
