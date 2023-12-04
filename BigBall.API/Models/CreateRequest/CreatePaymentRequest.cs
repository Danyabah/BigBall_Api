namespace BigBall.API.Models.CreateRequest
{
    /// <summary>
    /// Модель запроса создания способа оплаты
    /// </summary>
    public class CreatePaymentRequest
    {
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
