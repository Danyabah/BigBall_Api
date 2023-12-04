using BigBall.API.Models.CreateRequest;

namespace BigBall.API.Models.Request
{
    /// <summary>
    /// Модель запроса создания клиента
    /// </summary>
    public class PersonRequest : CreatePersonRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
