using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBall.Services.Contracts.Models
{
    /// <summary>
    /// Модель брони
    /// </summary>
    public class ReservationModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Модель <see cref="PersonModel"/>
        /// </summary>
        public PersonModel Person { get; set; }

        /// <summary>
        /// Модель <see cref="TrackModel"/>
        /// </summary>
        public TrackModel Track { get; set; }

        /// <summary>
        /// Модель <see cref="InstitutionModel"/>
        /// </summary>
        public InstitutionModel Institution { get; set; }

        /// <summary>
        /// Модель <see cref="PromotionModel"/>
        /// </summary>
        public PromotionModel? Promotion { get; set; }

        /// <summary>
        /// Модель <see cref="PaymentModel"/>
        /// </summary>
        public PaymentModel Payment { get; set; }

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
    }
}
