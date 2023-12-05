namespace BigBall.API.Models.Response
{
    /// <summary>
    /// Модель ответа заказа
    /// </summary>
    public class ReservationResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Модель <<see cref="PersonResponse"/>
        /// </summary>
        public PersonResponse Person { get; set; }

        /// <summary>
        /// Модель <<see cref="TrackResponse"/>
        /// </summary>
        public TrackResponse Track { get; set; }

        /// <summary>
        /// Модель <<see cref="InstitutionResponse"/>
        /// </summary>
        public InstitutionResponse Institution { get; set; }

        /// <summary>
        /// Модель <<see cref="PromotionResponse"/>
        /// </summary>
        public PromotionResponse? Promotion { get; set; }

        /// <summary>
        /// Модель <<see cref="PaymentResponse"/>
        /// </summary>
        public PaymentResponse Payment { get; set; }

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
