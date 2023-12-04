using BigBall.General;

namespace BigBall.Services.Contracts.Exeptions
{
    /// <summary>
    /// Ошибки валидации
    /// </summary>
    public class TimeTableValidationException : TimeTableException
    {
        /// <summary>
        /// Ошибки
        /// </summary>
        public IEnumerable<InvalidateItemModel> Errors { get; }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="AdministrationValidationException"/>
        /// </summary>
        public TimeTableValidationException(IEnumerable<InvalidateItemModel> errors)
        {
            Errors = errors;
        }
    }
}
