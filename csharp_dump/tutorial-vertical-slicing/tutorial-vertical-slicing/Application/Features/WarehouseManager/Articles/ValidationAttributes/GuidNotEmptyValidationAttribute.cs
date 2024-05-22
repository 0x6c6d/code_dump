using System.ComponentModel.DataAnnotations;

namespace Application.Features.WarehouseManager.Articles.ValidationAttributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class GuidNotEmptyValidationAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is Guid guid)
            return guid != Guid.Empty;

        return false;
    }
}
