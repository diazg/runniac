using Runniac.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Data.Repositories
{
    public interface IEventRepository : IRepository<Event>
    {
        /// <summary>
        /// Busca todos los eventos o carreras en base a los parámetros recibidos. Si todos los parámetros son nulos,
        /// se retornarán todos los eventos presentes en la base de datos cuya fecha de celebración sea igual o posterior
        /// al día de hoy.
        /// </summary>
        /// <param name="location">Lugar de celebración.</param>
        /// <param name="eventType">Tipo de evento.</param>
        /// <param name="eventDate">Fecha hasta de celebración del evento.</param>
        /// <returns>La lista de eventos que coincida con los criterios de búsqueda.</returns>
        IEnumerable<Event> Find(string location, string eventType, DateTime? eventDate);

        /// <summary>
        /// Retorna todos los lugares diferentes asociados a carreras cuya fecha de celebración es posterior o igual al día
        /// de hoy.
        /// </summary>
        /// <returns>Lista de localidades en los que se han encontrado carreras.</returns>
        IEnumerable<string> GetLocations(string query);

        /// <summary>
        /// Retorna todos los eventos ordenados.
        /// </summary>
        /// <returns>La lista de eventos ordenados.</returns>
        IEnumerable<Event> GetAllOrdered();

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
    }
}
