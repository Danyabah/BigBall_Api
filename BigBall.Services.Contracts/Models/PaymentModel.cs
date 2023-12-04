namespace BigBall.Services.Contracts.Models
{
    /// <summary>
    /// Модель способа оплаты
    /// </summary>
    public class PaymentModel
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
        /// Реквизиты
        /// </summary>
        public string Requisites { get; set; } = string.Empty;

        /// <summary>
        /// Номер карты
        /// </summary>
        public string CardNumber { get; set; } = string.Empty;

    }
}
