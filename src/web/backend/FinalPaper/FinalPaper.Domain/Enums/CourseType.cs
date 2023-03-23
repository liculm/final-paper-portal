using FinalPaper.Domain.Entities.Base;
using FinalPaper.Domain.Utility;

namespace FinalPaper.Domain.Enums;

public class CourseType : Enumeration
{
    public static CourseType Preddiplomski = new(1, "Preddiplomski");
    public static CourseType Diplomski = new(2, "Diplomski");

    public CourseType() : base()
    {
    }

    public CourseType(int id, string name) : base(id, name)
    {
    }
}