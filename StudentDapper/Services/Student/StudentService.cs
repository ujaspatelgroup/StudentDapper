using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using StudentDapper.Data;
using StudentDapper.DTOs.Shared;
using StudentDapper.DTOs.Student;
using System.Collections.Generic;
using System.Data;

namespace StudentDapper.Services.Student
{
    public class StudentService : IStudentService
    {

        private readonly IApplicationContextDapper _context;

        public StudentService(IApplicationContextDapper context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<GetStudentDto>>> GetAllStudentsAsync()
        {
            var serviceresponse = new ServiceResponse<List<GetStudentDto>>();
            var query = "Select [Id], [Name], [Address] From Students";
            var students = await _context.GetDataAsync<GetStudentDto>(query);
            serviceresponse.data = students.ToList();
            return serviceresponse;
        }

        public async Task<ServiceResponse<GetStudentDto>> GetStudentAsync(int id)
        {
            var serviceresponse = new ServiceResponse<GetStudentDto>();
            var findstudent = await findStudent(id);
            if (findstudent is not null)
            {
                serviceresponse.data = findstudent;
                return serviceresponse;
            }
            else
            {
                serviceresponse.Success = false;
                serviceresponse.Message = "Student not found";
                return serviceresponse;
            }
        }

        public async Task<ServiceResponse<List<GetStudentDto>>> AddStudentAsync(AddStudentDto _student)
        {
            var serviceresponse = new ServiceResponse<List<GetStudentDto>>();
            var query = "INSERT INTO Students ([Name], [Address]) VALUES (@Name, @Address)";

            var parameters = new DynamicParameters();
            parameters.Add("Name", _student.Name, DbType.String);
            parameters.Add("Address", _student.Address, DbType.String);
            bool result = await _context.ExecuteSqlAsync<bool>(query, parameters);

            if (result)
            {
                var students = await GetAllStudentsAsync();
                serviceresponse.data = students.data;
                return serviceresponse;
            }
            else
            {
                serviceresponse.Success = false;
                serviceresponse.Message = "Student not added";
                return serviceresponse;
            }
        }

        public async Task<ServiceResponse<GetStudentDto>> UpdateStudentAsync(UpdateStudentDto _student)
        {
            var serviceresponse = new ServiceResponse<GetStudentDto>();
            var findstudent = await findStudent(_student.Id);
            if (findstudent is null)
            {
                serviceresponse.Success = false;
                serviceresponse.Message = "Student not found";
                return serviceresponse;
            }

            var query = "UPDATE Students SET [Name] = @Name, [Address] = @Address WHERE [Id] = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", _student.Id, DbType.Int64);
            parameters.Add("Name", _student.Name, DbType.String);
            parameters.Add("Address", _student.Address, DbType.String);

            bool result = await _context.ExecuteSqlAsync<bool>(query, parameters);
            if (result)
            {
                var studentResult = await findStudent(_student.Id);
                serviceresponse.data = studentResult;
                return serviceresponse;
            }
            else
            {
                serviceresponse.Success = false;
                serviceresponse.Message = "Student not updated";
                return serviceresponse;
            }
        }

        public async Task<ServiceResponse<List<GetStudentDto>>> DeleteStudentAsync(int id)
        {
            var serviceresponse = new ServiceResponse<List<GetStudentDto>>();

            var findstudent = await findStudent(id);
            if (findstudent is null)
            {
                serviceresponse.Success = false;
                serviceresponse.Message = "Student not found";
                return serviceresponse;
            }

            var query = "DELETE FROM Students WHERE [Id] = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int64);

            bool result = await _context.ExecuteSqlAsync<bool>(query, parameters);
            if (result)
            {
                var students = await GetAllStudentsAsync();
                serviceresponse.data = students.data;
                return serviceresponse;
            }
            else
            {
                serviceresponse.Success = false;
                serviceresponse.Message = "Student not deleted";
                return serviceresponse;
            }
        }

        private async Task<GetStudentDto> findStudent(int id)
        {
            GetStudentDto? student = null;

            var query = "Select [Id], [Name], [Address] From Students Where [Id] = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int64);

            student = await _context.GetDataSingleAsync<GetStudentDto>(query,parameters);
            return student;
        }
    }
}
