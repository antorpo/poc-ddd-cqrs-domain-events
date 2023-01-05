namespace Application.Common.Interfaces
{
    public interface IEmailService
    {
        /// <summary>
        /// Metodo que envia un correo.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendEmail(string email, string subject, string message);
    }
}
