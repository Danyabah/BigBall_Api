namespace BigBall.Services.Contracts.Exeptions
{
    public abstract class BigBallException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="BigBallException"/> без параметров
        /// </summary>
        protected BigBallException() { }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="BigBallException"/> с указанием
        /// сообщения об ошибке
        /// </summary>
        protected BigBallException(string message)
            : base(message) { }
    }
}
