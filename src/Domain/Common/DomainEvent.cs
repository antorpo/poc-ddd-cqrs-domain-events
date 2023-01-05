using MediatR;

namespace Domain.Common
{
    /// <summary>
    /// Clase base para los eventos de dominio que implementa la (Marker Interface) INotification.
    /// </summary>
    public abstract class DomainEvent : INotification
    {
        protected DomainEvent()
        {
            DateOccurred = DateTime.Now;
        }

        /// <summary>
        /// Indica si el evento fue publicado.
        /// </summary>
        public bool IsPublished { get; set; }

        /// <summary>
        /// Fecha en que ocurre el evento.
        /// </summary>
        public DateTime DateOccurred { get; protected set; } = DateTime.Now;
    }

    /// <summary>
    /// Interfaz para los agregados que tienen eventos de dominio.
    /// </summary>
    public interface IHasDomainEvent
    {
        // Almacenamos los eventos para cada agregado en una lista.
        // Estos seran publicados por el Mediator a su respectivo EventHandler.
        // El EventHandler se encarga de ejecutar el código necesario para reaccionar a cada evento.
        // El despacho de los eventos depende el enfoque que utilicemos (Deferred Execution, Immediate Execution).
        // https://lostechies.com/jimmybogard/2014/05/13/a-better-domain-events-pattern/
        public List<DomainEvent> DomainEvents { get; set; }
    }
}