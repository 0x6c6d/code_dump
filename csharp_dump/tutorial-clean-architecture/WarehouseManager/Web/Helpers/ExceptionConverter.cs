namespace Web.Helpers;

public class ExceptionConverter
{
    public static DataAccessResponse<TEntity> ConvertDataAccessExceptions<TEntity>(DataAccessException ex)
    {
        if (ex.StatusCode == 400)
        {
            return new DataAccessResponse<TEntity>() { Message = "Validierungsfehler:", ValidationErrors = ex.Response, Success = false };
        }
        else if (ex.StatusCode == 404)
        {
            return new DataAccessResponse<TEntity>() { Message = "Das angefragte Object wurde nicht gefunden.", Success = false };
        }
        else
        {
            return new DataAccessResponse<TEntity>() { Message = "Es ist etwas schief gelaufen. Bitte versuchen Sie es später erneut.", Success = false };
        }
    }
}