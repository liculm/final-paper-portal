using FinalPaper.Infrastructure;
using Microsoft.EntityFrameworkCore.Design;

namespace FinalPaper.Migration; 

public class FinalPaperDbContextMigration : IDesignTimeDbContextFactory<FinalPaperContext>{
    public FinalPaperContext CreateDbContext(string[] args)
    {
        return new MigrationDbContextBuilder<FinalPaperContext>().CreateDbContext(new[]
             { "FinalPaperRuntimeDb" });
    }
}