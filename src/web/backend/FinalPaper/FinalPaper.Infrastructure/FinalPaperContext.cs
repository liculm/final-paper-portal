using FinalPaper.Domain.Entities;
using FinalPaper.Domain.Enums;
using FinalPaper.Domain.Utility;
using FinalPaper.Infrastructure.EntityConfigurations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FinalPaper.Infrastructure; 

public class FinalPaperContext : BaseDbContext
{
    public FinalPaperContext(DbContextOptions<FinalPaperContext> options) : base(options)
    {
    }

    public FinalPaperContext(DbContextOptions<FinalPaperContext> options, IMediator mediator, IDateTime dateTime) :
        base(options, mediator, dateTime)
    {
    }

    public DbSet<User>? Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EntityConfiguration<>).Assembly);
        modelBuilder.Entity<User>(builder =>
        {
            // Date is a DateOnly property and date on database
            // builder.Property(x => x.Date)
            //     .HasConversion<DateOnlyConverter, DateOnlyComparer>();
        });

        SeedEnums(modelBuilder);
    }

    private void SeedEnums(ModelBuilder builder)
    {
        builder.Entity<Roles>().HasData(Enumeration.GetAll<Roles>()
            .Select(x => new Roles(x.Id, x.Name)).ToArray());
    }
}

public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
{
    public DateOnlyConverter() : base(
        dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
        dateTime => DateOnly.FromDateTime(dateTime))
    {
    }
}

public class DateOnlyComparer : ValueComparer<DateOnly>
{
    public DateOnlyComparer() : base(
        (d1, d2) => d1.DayNumber == d2.DayNumber,
        d => d.GetHashCode())
    {
    }
}