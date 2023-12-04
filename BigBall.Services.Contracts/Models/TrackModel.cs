using BigBall.Services.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBall.Services.Contracts.Models
{
    /// <summary>
    /// Модель дорожки
    /// </summary>
    public class TrackModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Вместимость
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// <<see cref="TrackModelType"/>
        /// </summary>
        public TrackModelType Type { get; set; } = TrackModelType.Polished;
    }
}
