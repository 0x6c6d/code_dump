using System.ComponentModel.DataAnnotations;

namespace Application.Features.WarehouseManager.Articles.ValidationAttributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class MinStockValidationAttribute : ValidationAttribute
{
    private readonly string _categoryId;

    public MinStockValidationAttribute(string categoryId)
    {
        _categoryId = categoryId;
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("Value is null");
        }

        var property = validationContext.ObjectType.GetProperty(_categoryId);
        if (property == null)
        {
            return new ValidationResult($"Unknown property: {_categoryId}");
        }

        var categoryId = property.GetValue(validationContext.ObjectInstance);
        if (categoryId is Guid guid)
        {
            if (guid == Guid.Empty && (int)value < 1)
            {
                return new ValidationResult("Der Mindestbestand muss min. 1 betragen.");
            }
        }

        return ValidationResult.Success!;
    }
}
