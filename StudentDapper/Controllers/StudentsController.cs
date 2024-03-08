using Microsoft.AspNetCore.Mvc;
using StudentDapper.DTOs.Shared;
using StudentDapper.DTOs.Student;
using StudentDapper.Services.Student;

namespace StudentDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ServiceResponse<List<GetStudentDto>>> Get()
        {
            return await _studentService.GetAllStudentsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ServiceResponse<GetStudentDto>> Get(int id)
        {
            return await _studentService.GetStudentAsync(id);
        }

        [HttpPost]
        public async Task<ServiceResponse<List<GetStudentDto>>> AddStudent(AddStudentDto student)
        {
            return await _studentService.AddStudentAsync(student);
        }

        [HttpPut]
        public async Task<ServiceResponse<GetStudentDto>> UpdateStudent(UpdateStudentDto student)
        {
            return await _studentService.UpdateStudentAsync(student);
        }

        [HttpDelete]
        public async Task<ServiceResponse<List<GetStudentDto>>> DeleteStudent(int id)
        {
            return await _studentService.DeleteStudentAsync(id);
        }
    }
}
