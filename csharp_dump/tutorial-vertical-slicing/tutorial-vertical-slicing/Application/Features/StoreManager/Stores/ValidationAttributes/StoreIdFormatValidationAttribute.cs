using System.ComponentModel.DataAnnotations;

namespace Application.Features.StoreManager.Stores.ValidationAttributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class StoreIdFormatValidationAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        string storeId = value?.ToString() ?? string.Empty;
        if (string.IsNullOrEmpty(storeId))
        {
            return false;
        }

        if (storeId.Length != 6)
        {
            return false;
        }

        if (!storeId.All(char.IsDigit))
        {
            return false;
        }

        if (!storeId.StartsWith("010") && !storeId.StartsWith("030"))
        {
            return false;
        }

        return true;
    }
}
