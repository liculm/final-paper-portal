using FinalPaper.Domain.Utility;

namespace FinalPaper.Domain.Enums; 

public class Roles : Enumeration{
    public static Roles Admin = new(1, "Admin");
    public static Roles Student = new(2, "Student");
    public static Roles Mentor = new(3, "Mentor");
    public Roles(int id, string name) : base(id, name) {
        Id = id;
        Name = name;
    }

    public Roles():base()
    {
    }

}