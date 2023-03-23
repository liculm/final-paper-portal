using FinalPaper.Domain.Utility;

namespace FinalPaper.Domain.Enums;

public class Roles : Enumeration
{
    public static Roles Admin = new(1, "Admin");
    public static Roles Mentor = new(2, "Mentor");
    public static Roles Student = new(3, "Student");

    public Roles() : base()
    {
    }

    public Roles(int id, string name) : base(id, name)
    {
    }
}