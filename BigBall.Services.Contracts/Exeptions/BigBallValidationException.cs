using BigBall.General;

namespace BigBall.Services.Contracts.Exeptions
{
    /// <summary>
    /// Ошибки валидации
    /// </summary>
    public class BigBallValidationException : BigBallException
    {
        /// <summary>
        /// Ошибки
        /// </summary>
        public IEnumerable<InvalidateItemModel> Errors { get; }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="AdministrationValidationException"/>
        /// </summary>
        public BigBallValidationException(IEnumerable<InvalidateItemModel> errors)
        {
            Errors = errors;
        }
    }
}
