namespace SimpleBookingSystem.Infrastructure.Interfaces
{
    public interface IRepository<T>
    {
        Task SaveChangesAsync();
    }
}
