namespace FinalPaper.Migration; 

public class Program
{
    static void Main(string[] args)
    {
//TODO: Fix this
        Console.WriteLine("Creating context");
        // var context = new MigrationDbContextBuilder<ClearinghouseDbContext>().CreateDbContext(new[] { "ClearinghouseRuntimeDb" });

        Console.WriteLine("Migrating database");
        // context.Database.Migrate();

        Console.WriteLine("Completed");
        Environment.Exit(0);
    }
}