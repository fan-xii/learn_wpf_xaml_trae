namespace LearnXaml.Models;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Grade { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsEnrolled { get; set; } = true;
    public DateTime EnrollmentDate { get; set; } = DateTime.Now;
}