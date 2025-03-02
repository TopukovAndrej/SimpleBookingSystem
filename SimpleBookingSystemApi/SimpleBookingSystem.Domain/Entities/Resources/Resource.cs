namespace SimpleBookingSystem.Domain.Entities.Resources
{
    using SimpleBookingSystem.Contracts.Models;
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

        public Result UpdateTotalQuantity(int bookedQuantity)
        {
            int updatedQuantity = TotalQuantity - bookedQuantity;

            if (updatedQuantity < 0)
            {
                return Result.Failed(errorMessage: "Resource quantity is less than zero!");
            }

            TotalQuantity = updatedQuantity;

            return Result.Success();
        }
    }
}
