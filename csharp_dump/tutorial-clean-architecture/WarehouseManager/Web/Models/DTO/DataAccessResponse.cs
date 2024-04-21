namespace Web.Models.DTO;

public class DataAccessResponse<T>
{
    public string Message { get; set; } = string.Empty;
    public string? ValidationErrors { get; set; }
    public bool Success { get; set; } = true;
    public T? Data { get; set; }
}