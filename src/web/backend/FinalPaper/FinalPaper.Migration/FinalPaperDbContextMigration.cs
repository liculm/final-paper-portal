using FinalPaper.Infrastructure;
using Microsoft.EntityFrameworkCore.Design;

namespace FinalPaper.Migration; 

public class FinalPaperDbContextMigration : IDesignTimeDbContextFactory<FinalPaperDBContext>{
    public FinalPaperDBContext CreateDbContext(string[] args)
    {
        return new MigrationDbContextBuilder<FinalPaperDBContext>().CreateDbContext(new[]
             { "FinalPaperRuntimeDb" });
    }
}