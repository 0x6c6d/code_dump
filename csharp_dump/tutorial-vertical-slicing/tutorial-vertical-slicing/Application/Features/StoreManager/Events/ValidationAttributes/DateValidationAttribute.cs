using System.ComponentModel.DataAnnotations;

namespace Application.Features.StoreManager.Events.ValidationAttributes;
public class DateValidationAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is DateOnly date)
        {
            return date > DateOnly.FromDateTime(DateTime.Today);
        }

        return false;
    }
}
