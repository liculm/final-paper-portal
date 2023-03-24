using FinalPaper.Domain.Utility;

namespace FinalPaper.Domain.Enums;

public class CourseTypes : Enumeration
{
    public static CourseTypes Preddiplomski = new(1, "Preddiplomski");
    public static CourseTypes Diplomski = new(2, "Diplomski");

    public CourseTypes() : base()
    {
    }

    public CourseTypes(int id, string name) : base(id, name)
    {
    }
}