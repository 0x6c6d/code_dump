using System.ComponentModel.DataAnnotations;

namespace Application.Features.WarehouseManager.Articles.ValidationAttributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class StoragePlaceInUseValidationAttribute : ValidationAttribute
{
    private readonly string _storagePlace;

    public StoragePlaceInUseValidationAttribute(string storagePlace)
    {
        _storagePlace = storagePlace;
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("Value is null");
        }

        var property = validationContext.ObjectType.GetProperty(_storagePlace);
        if (property == null)
        {
            return new ValidationResult($"Unknown property: {_storagePlace}");
        }

        var storagePlace = property.GetValue(validationContext.ObjectInstance)?.ToString() ?? string.Empty;
        if (!string.IsNullOrEmpty(storagePlace) && storagePlace == "Im Einsatz")
        {
            if (string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult("Bitte wählen Sie eine Filiale aus.");
            }
        }

        return ValidationResult.Success!;
    }
}
