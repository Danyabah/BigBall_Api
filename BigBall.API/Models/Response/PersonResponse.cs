namespace BigBall.API.Models.Response
{
    /// <summary>
    /// Модель ответа клиента
    /// </summary>
    public class PersonResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Возраст
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        public string Email { get; set; } = string.Empty;
    }
}
