using Domain.Enums;

namespace Domain.Exceptions
{
    /// <summary>
    /// Clase que representa una excepción de negocio.
    /// </summary>
    public class BusinessException : Exception
    {
        public BusinessException(BusinessExceptions businessExceptions, params object[] args) : base(businessExceptions.GetDescription(args))
        {
        }
    }
}
