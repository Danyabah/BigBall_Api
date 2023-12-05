namespace BigBall.Services.Contracts.Models
{
    /// <summary>
    /// Модель клуба
    /// </summary>
    public class InstitutionModel
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
        public string OpeningTime { get; set; }

        /// <summary>
        /// Время закрытия
        /// </summary>
        public string ClosingTime { get; set; }
    }
}
