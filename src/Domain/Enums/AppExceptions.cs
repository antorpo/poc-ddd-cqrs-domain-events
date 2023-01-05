namespace Domain.Enums
{
    /// <summary>
    /// Clase que contiene los tipos de excepciones de aplicación.
    /// </summary>
    public class AppExceptions : Enumeration
    {
        public static AppExceptions ValidationError = new(0, nameof(ValidationError), "Error de validación para el objeto [{0}].");

        public static AppExceptions NotFound = new(1, nameof(NotFound), "No se encontró el objeto.");

        public AppExceptions(int id, string name, string description) : base(id, name, description) { }
    }
}
