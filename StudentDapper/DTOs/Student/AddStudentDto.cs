using System.ComponentModel.DataAnnotations;

namespace StudentDapper.DTOs.Student
{
    public class AddStudentDto
    {
        [Required(ErrorMessage = "Student name is required")]
        public required string Name { get; set; }

        public string? Address { get; set; }
    }
}
