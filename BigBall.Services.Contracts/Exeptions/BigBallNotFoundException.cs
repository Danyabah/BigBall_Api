namespace BigBall.Services.Contracts.Exeptions
{
    public class BigBallNotFoundException : BigBallException
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="BigBallNotFoundException"/> с указанием
        /// сообщения об ошибке
        /// </summary>
        public BigBallNotFoundException(string message)
            : base(message)
        { }
    }
}
