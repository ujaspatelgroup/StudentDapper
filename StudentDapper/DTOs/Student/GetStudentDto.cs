using System.ComponentModel.DataAnnotations;

namespace StudentDapper.DTOs.Student
{
    public class GetStudentDto
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public string? Address { get; set; }
    }
}