namespace SimpleBookingSystem.Domain.Common.Entities
{
    using System;

    public class AggregateRoot : Entity
    {
        public AggregateRoot(int id, Guid uid, bool isDeleted) : base(id: id, uid: uid, isDeleted: isDeleted) { }
    }
}
