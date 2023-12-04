using BigBall.Common.Entity.EntityInterfaces;

namespace BigBall.Services.Contracts.Exeptions
{
    public class BigBallEntityNotFoundException<TEntity> : BigBallNotFoundException
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="BigBallEntityNotFoundException{TEntity}"/>
        /// </summary>
        public BigBallEntityNotFoundException(Guid id)
            : base($"Сущность {typeof(TEntity)} c id = {id} не найдена.")
        {
        }
    }
}
