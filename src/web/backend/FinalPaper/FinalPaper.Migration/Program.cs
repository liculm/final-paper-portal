using FinalPaper.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FinalPaper.Migration; 

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Creating context");
        var context = new MigrationDbContextBuilder<FinalPaperDBContext>().CreateDbContext(new[] { "FinalPaperRuntimeDb" });

        Console.WriteLine("Migrating database");
        context.Database.Migrate();

        Console.WriteLine("Completed");
        Environment.Exit(0);
    }
}