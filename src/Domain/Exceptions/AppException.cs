using Domain.Enums;

namespace Domain.Exceptions
{
    /// <summary>
    /// Clase que representa una excepción de aplicación.
    /// </summary>
    public class AppException : Exception
    {
        public AppException(AppExceptions applicationException, params object[] args) : base(applicationException.GetDescription(args))
        {
        }
    }
}
