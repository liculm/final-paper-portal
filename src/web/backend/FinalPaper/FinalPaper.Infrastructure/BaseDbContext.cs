using FinalPaper.Domain.Entities.Base;
using FinalPaper.Domain.Utility;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FinalPaper.Infrastructure;

public abstract class BaseDbContext : DbContext
{
    private const string UnknownUser = "Unknown";
    private readonly IDateTime dateTime;
    private readonly IMediator mediator;
    // private readonly ADUser user;

    public BaseDbContext(DbContextOptions options) : this(options, new NoMediator(),
        new DefaultDateTime())
    {
    }

    public BaseDbContext(DbContextOptions options, IMediator mediator, IDateTime dateTime) :
        base(options)
    {
        this.mediator = mediator;
        this.dateTime = dateTime;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        SetCreatedAndModified();
        return await base.SaveChangesAsync(cancellationToken);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new())
    {
        SetCreatedAndModified();
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override int SaveChanges()
    {
        SetCreatedAndModified();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        SetCreatedAndModified();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public void DetachAllEntities()
    {
        ChangeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;
        ChangeTracker.DeleteOrphansTiming = CascadeTiming.OnSaveChanges;

        var entityEntries = ChangeTracker.Entries().ToArray();

        foreach (var entityEntry in entityEntries) entityEntry.State = EntityState.Detached;
    }

    private void SetCreatedAndModified()
    {
        var currentTime = dateTime.Now;
        foreach (var entity in ChangeTracker.Entries()
                     .Where(x => x.State == EntityState.Added && x.Entity is Entity)
                     .Select(x => x.Entity as Entity))
            entity?.SetCreatedAndModified(currentTime, UnknownUser);

        foreach (var entity in ChangeTracker.Entries()
                     .Where(x => x.State == EntityState.Modified && x.Entity is Entity)
                     .Select(x => x.Entity as Entity))
            entity?.SetModified(currentTime, UnknownUser);
    }
}

public class NoMediator : IMediator
{
    public Task Publish(object notification, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
        where TNotification : INotification
    {
        return Task.CompletedTask;
    }

    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request,
        CancellationToken cancellationToken = default)
    {
        var result = default(TResponse);
        if (result is null)
            throw new NullReferenceException(
                $"{nameof(BaseDbContext)} ${nameof(Send)} ${nameof(TResponse)} is null");

        return Task.FromResult<TResponse>(result);
    }

    public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = new CancellationToken()) where TRequest : IRequest {
        return Task.FromResult(default(object?));
    }

    public Task<object?> Send(object request, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(default(object?));
    }

    public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request,
        CancellationToken cancellationToken = new())
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<object?> CreateStream(object request,
        CancellationToken cancellationToken = new())
    {
        throw new NotImplementedException();
    }
}