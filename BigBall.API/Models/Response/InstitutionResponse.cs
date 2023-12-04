namespace BigBall.API.Models.Response
{
    /// <summary>
    /// Модель ответа клуба
    /// </summary>
    public class InstitutionResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Рабочая почта
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Время открытия
        /// </summary>
        public TimeOnly OpeningTime { get; set; }

        /// <summary>
        /// Время закрытия
        /// </summary>
        public TimeOnly ClosingTime { get; set; }
    }
}
