namespace BigBall.Common.Entity.EntityInterfaces
{
    /// <summary>
    /// Аудит сущности с Id
    /// </summary>
    public interface IEntityWithId
    {
        public Guid Id { get; set; }
    }
}
