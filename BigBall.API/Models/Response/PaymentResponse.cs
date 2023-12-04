namespace BigBall.API.Models.Response
{
    /// <summary>
    /// Модель ответа способа оплаты
    /// </summary>
    public class PaymentResponse
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
        /// Номер карты
        /// </summary>
        public string CardNumber { get; set; } = string.Empty;
    }
}
