using Runniac.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Business
{
    public interface ICommentService
    {
        /// <summary>
        /// Retorna todos los comentarios encontrados para un determinado evento.
        /// </summary>
        /// <returns>Una colección de comentarios.</returns>
        IEnumerable<Comment> GetCommentsByEvent(long eventId);


        /// <summary>
        /// Guarda un nuevo comentario.
        /// </summary>
        /// <param name="comment">Instancia del comentario a guardar.</param>
        void SaveComment(Comment comment);

        /// <summary>
        /// Crea un voto (positivo o negativo) asociado a un comentario.
        /// </summary>
        /// <param name="commentId">ID del comentario votado.</param>
        /// <returns>True si el voto se realiza correctamente.</returns>
        bool AddVote(Vote vote);

        /// <summary>
        /// Retorna un comentario a partir de su ID.
        /// </summary>
        /// <param name="commentId">ID del comentario a buscar.</param>
        /// <returns>El comentario si es encontrado y null en caso contrario.</returns>
        Comment GetComment(long commentId);

        /// <summary>
        /// Retorna todos los comentarios encontrados para un determinado usuario.
        /// </summary>
        /// <returns>Una colección de comentarios.</returns>        
        IEnumerable<Comment> GetCommentsByUser(int userId);

        /// <summary>
        /// Calcula la puntuación recibida por un comentario por parte de los usuarios que lo han votado
        /// positiva o negativamente.
        /// </summary>
        /// <param name="commentId">ID del comentario del que se va a calcular la puntuación.</param>
        /// <returns>La puntuación del comentario.</returns>
        int WorkoutRanking(long commentId);

        /// <summary>
        /// Borra todos los comentarios realizados por un usuario.
        /// </summary>
        /// <param name="userId">ID del usuario.</param>
        void DeleteComments(int userId);
    }
}
