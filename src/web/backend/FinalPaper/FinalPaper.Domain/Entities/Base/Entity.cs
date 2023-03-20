using FinalPaper.Domain.Attributes;

namespace FinalPaper.Domain.Entities.Base; 

[IgnoreCompare]
public abstract class Entity
{
    [IgnoreCompare] public string? CreatedByName { get; protected set; }
    [IgnoreCompare] public DateTime CreatedDateUtc { get; protected set; }
    [IgnoreCompare] public string? ModifiedByName { get; protected set; }
    [IgnoreCompare] public DateTime ModifiedDateUtc { get; protected set; }

    public void SetCreatedAndModified(DateTime currentTimeUtc, string username)
    {
        CreatedByName = username;
        CreatedDateUtc = currentTimeUtc;
        ModifiedByName = username;
        ModifiedDateUtc = currentTimeUtc;
    }

    public void SetModified(DateTime currentTimeUtc, string username)
    {
        ModifiedByName = username;
        ModifiedDateUtc = currentTimeUtc;
    }
}