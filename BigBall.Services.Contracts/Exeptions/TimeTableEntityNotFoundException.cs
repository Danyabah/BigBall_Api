using BigBall.Common.Entity.EntityInterfaces;

namespace BigBall.Services.Contracts.Exeptions
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="TimeTableEntityNotFoundException{TEntity}"/>
    /// </summary>
    public TimeTableEntityNotFoundException(Guid id)
        : base($"Сущность {typeof(IEntity)} c id = {id} не найдена.")
    {
    }
}
