﻿using Application.Features.WarehouseManager.Categories.Repositories;

namespace Application.Features.WarehouseManager.Categories.Operations.Update;
public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryRequest>
{
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryValidator(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;

        RuleFor(g => g.Name)
            .NotEmpty().WithMessage("{PropertyName} wird benötigt.")
            .NotNull()
            .MinimumLength(2).WithMessage("{PropertyName} muss mindestens 2 Zeichen lang sein.")
            .MaximumLength(100).WithMessage("{PropertyName} darf maximal 100 Zeichen lang sein.");

        RuleFor(a => a)
          .MustAsync(CategoryNameUnique)
          .WithMessage("Eine Kategorie mit demselben Namen existiert bereits.");
    }

    private async Task<bool> CategoryNameUnique(UpdateCategoryRequest e, CancellationToken token)
    {
        return !await _categoryRepository.IsCategoryNameUnique(e.Name);
    }
}
