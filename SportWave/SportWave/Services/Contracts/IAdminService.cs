﻿using SportWave.ViewModels.MenAndWomenViewModels;

namespace SportWave.Services.Contracts
{
    public interface IAdminService
    {
        Task AddCategoryAsync(AddCategoryViewModel model);
        Task AddProductAsync(AddProductViewModel model);
        Task<AddProductViewModel> GetNewAddedProductAsync();
    }
}
