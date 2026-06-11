using System.Collections.ObjectModel;
using LearnXaml.Models;

namespace LearnXaml.ViewModels;

public class StudentViewModel : ObservableObject
{
    private Student? _selectedStudent;
    private string _nameFilter = string.Empty;

    public ObservableCollection<Student> Students { get; } = new()
    {
        new() { Id = 1, Name = "张三", Age = 20, Grade = "大三", Email = "zhangsan@example.com", IsEnrolled = true },
        new() { Id = 2, Name = "李四", Age = 21, Grade = "大四", Email = "lisi@example.com", IsEnrolled = true },
        new() { Id = 3, Name = "王五", Age = 19, Grade = "大二", Email = "wangwu@example.com", IsEnrolled = false },
        new() { Id = 4, Name = "赵六", Age = 22, Grade = "研一", Email = "zhaoliu@example.com", IsEnrolled = true },
        new() { Id = 5, Name = "孙七", Age = 20, Grade = "大三", Email = "sunqi@example.com", IsEnrolled = true },
    };

    public Student? SelectedStudent
    {
        get => _selectedStudent;
        set => SetProperty(ref _selectedStudent, value);
    }

    public string NameFilter
    {
        get => _nameFilter;
        set => SetProperty(ref _nameFilter, value);
    }

    public RelayCommand AddStudentCommand { get; }
    public RelayCommand DeleteStudentCommand { get; }
    public RelayCommand ClearCommand { get; }

    public StudentViewModel()
    {
        AddStudentCommand = new RelayCommand(AddStudent);
        DeleteStudentCommand = new RelayCommand(DeleteStudent, _ => SelectedStudent != null);
        ClearCommand = new RelayCommand(Clear);
    }

    private void AddStudent(object? _)
    {
        var newStudent = new Student
        {
            Id = Students.Count + 1,
            Name = "新同学",
            Age = 20,
            Grade = "大一",
            Email = "new@example.com"
        };
        Students.Add(newStudent);
        SelectedStudent = newStudent;
    }

    private void DeleteStudent(object? _)
    {
        if (SelectedStudent != null)
            Students.Remove(SelectedStudent);
    }

    private void Clear(object? _)
    {
        Students.Clear();
    }
}