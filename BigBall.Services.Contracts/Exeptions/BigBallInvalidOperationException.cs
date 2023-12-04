namespace BigBall.Services.Contracts.Exeptions
{
    public class BigBallInvalidOperationException : BigBallException
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="BigBallInvalidOperationException"/>
        /// с указанием сообщения об ошибке
        /// </summary>
        public BigBallInvalidOperationException(string message)
            : base(message)
        {

        }
    }
}
