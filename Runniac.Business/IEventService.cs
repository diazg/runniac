using Runniac.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Business
{
    public interface IEventService
    {
        /// <summary>
        /// Guarda un nuevo evento.
        /// </summary>
        /// <param name="eventObj">Instancia del evento a guardar.</param>
        void SaveEvent(Event eventObj);

        /// <summary>
        /// Retorna todos los eventos encontrados.
        /// </summary>
        /// <returns>Una colección que contiene todos los eventos.</returns>
        IEnumerable<Event> GetAllEvents();

        /// <summary>
        /// Realiza una búsqueda entre los eventos disponibles.
        /// </summary>
        /// <param name="location">Lugar en el que se va a celebrar la carrera.</param>
        /// <param name="eventType">Tipo de la carrera (ej: maratón, media maratón, etc.)</param>
        /// <param name="eventDate">Fecha tope para la búsqueda de eventos. Es decir, se retornarán todos los eventos cuya 
        /// fecha de celebración sea anterior o igual a la fecha pasada como parámetro.</param>
        /// <returns>La lista de eventos que cumplan los criterios de búsqueda.</returns>
        IEnumerable<Event> SearchEvents(string location, string eventType, string eventDate);

        /// <summary>
        /// Retorna todos los lugares diferentes en los que habrá carreras próximamente (a partir del día de hoy inclusive).
        /// </summary>
        /// <returns>Lista de localidades en los que se han encontrado carreras.</returns>
        IEnumerable<string> GetLocations(string query);

        /// <summary>
        /// Obtiene el evento cuyo ID coincida con el recibido como parámetro.
        /// </summary>
        /// <param name="id">ID del evento a retornar.</param>
        /// <returns>Instancia del evento encontrado.</returns>
        Event GetEventById(long id);

        /// <summary>
        /// Borra un evento a partir de su ID.
        /// </summary>
        /// <param name="id">ID del evento a borrar.</param>
        void DeleteEvent(long id);

        /// <summary>
        /// Modifica un evento.
        /// </summary>
        /// <param name="eventModel">Evento con la información a modificar.</param>
        void UpdateEvent(Event eventModel);

        /// <summary>
        /// Retorna una lista con los eventos más cercanos a la localización del usuario.
        /// </summary>
        /// <param name="lat">Latitud de la ubicación del usuario.</param>
        /// <param name="lon">Longitud de la ubicación del usuario.</param>
        /// <returns>La lista de eventos.</returns>
        IEnumerable<Event> GetClosest(decimal lat, decimal lon);

        /// <summary>
        /// Retorna la lista de los eventos con la mejor puntuación por parte de los usuarios.
        /// </summary>
        /// <returns>La lista de eventos</returns>
        IEnumerable<Event> GetTopRated();

        /// <summary>
        /// Calcula la puntuación de un evento en base a los comentarios recibidos por los
        /// usuarios.
        /// </summary>
        /// <param name="e">Evento del que se desea calcular la puntuación.</param>
        /// <returns>La puntuación del evento.</returns>
        float GetRating(Event e);
    }
}
