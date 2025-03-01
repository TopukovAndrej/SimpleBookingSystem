namespace SimpleBookingSystem.Application.Queries.Resource
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using SimpleBookingSystem.Contracts.Dtos.Resource;
    using SimpleBookingSystem.Contracts.Models;
    using SimpleBookingSystem.Infrastructure.Interfaces;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetAllResourcesQuery : IRequest<Result<IReadOnlyList<ResourceDto>>>
    {
        public GetAllResourcesQuery() { }
    }

    public class GetAllResourcesQueryHandler(ISimpleBookingSystemReadonlyDbContext dbContext) : IRequestHandler<GetAllResourcesQuery, Result<IReadOnlyList<ResourceDto>>>
    {
        public async Task<Result<IReadOnlyList<ResourceDto>>> Handle(GetAllResourcesQuery query, CancellationToken cancellationToken)
        {
            List<ResourceDto> result = await dbContext.Resources.Where(predicate: x => !x.IsDeleted)
                                                                .Select(selector: x => new ResourceDto() { Id = x.Id, Name = x.Name, TotalQuantity = x.TotalQuantity })
                                                                .ToListAsync(cancellationToken: cancellationToken);

            if (result.Count == 0)
            {
                return Result<IReadOnlyList<ResourceDto>>.Failed(errorMessage: "No resources found!");
            }

            return Result<IReadOnlyList<ResourceDto>>.Success(value: result);
        }
    }
}
