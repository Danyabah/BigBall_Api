namespace BigBall.API.Models.CreateRequest
{
    /// <summary>
    /// Модель запроса создания заказа
    /// </summary>
    public class CreateReservationRequest
    {
        /// <summary>
        /// Идентификатор заказчика
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// Идентификатор дорожки
        /// </summary>
        public Guid TrackId { get; set; }

        /// <summary>
        /// Идентификатор клуба
        /// </summary>
        public Guid InstitutionId { get; set; }

        /// <summary>
        /// Идентификатор промокода
        /// </summary>
        public Guid? PromotionId { get; set; }

        /// <summary>
        /// Идентификатор способа оплаты
        /// </summary>
        public Guid PaymentId { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        public DateTimeOffset Date { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Описание к брони
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Кол-во людей
        /// </summary>
        public int CountPeople { get; set; }
    }
}
