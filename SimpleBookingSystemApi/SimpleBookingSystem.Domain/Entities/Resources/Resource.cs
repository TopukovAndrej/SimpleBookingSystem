namespace SimpleBookingSystem.Domain.Entities.Resources
{
    using SimpleBookingSystem.Domain.Common.Entities;

    public class Resource : AggregateRoot
    {
        public string Name { get; private set; }

        public int TotalQuantity { get; private set; }

        public Resource(int id, Guid uid, bool isDeleted, string name, int totalQuality) : base(id: id, uid: uid, isDeleted: isDeleted)
        {
            Name = name;
            TotalQuantity = totalQuality;
        }
    }
}
