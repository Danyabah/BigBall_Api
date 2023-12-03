namespace BigBall.Context.Contracts.Models
{
    /// <summary>
    /// Сущность брони
    /// </summary>
    public class Reservation : BaseAuditEntity
    {
        /// <summary>
        /// Идентификатор <<see cref="Person"/>
        /// </summary>
        public Guid PersonId { get; set; }
        public Person Person { get; set; }

        /// <summary>
        /// Идентификатор <<see cref="Track"/>
        /// </summary>
        public Guid TrackId { get; set; }
        public Track Track { get; set; }

        /// <summary>
        /// Идентификатор <<see cref="Institution"/>
        /// </summary>
        public Guid InstitutionId { get; set; }
        public Institution Institution { get; set; }

        /// <summary>
        /// Идентификатор <<see cref="Promotion"/>
        /// </summary>
        public Guid? PromotionId { get; set; }
        public Promotion? Promotion { get; set; }

        /// <summary>
        /// Идентификатор <<see cref="Payment"/>
        /// </summary>
        public Guid PaymentId { get; set; }
        public Payment Payment { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        public DateOnly Date { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Описание к брони
        /// </summary>
        public string? Description { get; set; }
    }
}
