namespace Domain.Enums
{
    /// <summary>
    /// Clase que contiene los tipos de excepciones de negocio.
    /// </summary>
    public class BusinessExceptions : Enumeration
    {
        public static BusinessExceptions NotControlledException = new(0, nameof(NotControlledException), "Excepción no controlada.");
        public BusinessExceptions(int id, string name, string description) : base(id, name, description) { }
    }
}
