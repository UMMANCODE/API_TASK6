using System;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Helpers;
using TASK3_Business.Dtos.StudentDtos;
using TASK3_Business.Exceptions;
using TASK3_Business.Services.Interfaces;
using TASK3_Core.Entities;
using TASK3_DataAccess;
using TASK3_DataAccess.Repositories.Interfaces;

namespace TASK3_Business.Services.Implementations {
  public class StudentService : IStudentService {
    private readonly IStudentRepository _StudentRepository;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;

    public StudentService(IStudentRepository StudentRepository, IMapper mapper, IWebHostEnvironment env) {
      _StudentRepository = StudentRepository;
      _mapper = mapper;
      _env = env;
    }

    public async Task<int> Create(StudentCreateOneDto dto) {
      if (await _StudentRepository.ExistsAsync(x => x.Email == dto.Email && !x.IsDeleted))
        throw new RestException(StatusCodes.Status400BadRequest, "Email", "Dublicate Email values");

      var entity = _mapper.Map<Student>(dto);

      if (dto.Photo != null)
        entity.Image = FileManager.Save(dto.Photo, _env.WebRootPath, "images/students");

      await _StudentRepository.AddAsync(entity);
      await _StudentRepository.SaveAsync();

      return entity.Id;
    }
    public async Task<List<StudentGetAllDto>> GetAll(int pageNumber = 1, int pageSize = 1) {
      if (pageNumber <= 0 || pageSize <= 0) {
        throw new RestException(StatusCodes.Status400BadRequest, "Invalid parameters for paging");
      }

      var Students = await _StudentRepository.GetAllAsync(x => !x.IsDeleted, pageNumber, pageSize);
      return _mapper.Map<List<StudentGetAllDto>>(Students);
    }
    public async Task<StudentGetOneDto> GetById(int id) {
      var Student = await _StudentRepository.GetAsync(x => x.Id == id && !x.IsDeleted);

      return Student == null
        ? throw new RestException(StatusCodes.Status404NotFound, "Student not found")
        : _mapper.Map<StudentGetOneDto>(Student);
    }
    public async Task Update(int id, StudentUpdateOneDto updateDto) {
      var Student = await _StudentRepository.GetAsync(x => x.Id == id && !x.IsDeleted)
        ?? throw new RestException(StatusCodes.Status404NotFound, "Student not found");

      if (Student.Email != updateDto.Email && await _StudentRepository.ExistsAsync(x => x.Email == updateDto.Email && !x.IsDeleted))
        throw new RestException(StatusCodes.Status400BadRequest, "Email", "Dublicate Email values");

      Student.FirstName = updateDto.FirstName;
      Student.LastName = updateDto.LastName;
      Student.Email = updateDto.Email;
      Student.Phone = updateDto.Phone;
      Student.Address = updateDto.Address;
      Student.BirthDate = updateDto.BirthDate;
      Student.GroupId = updateDto.GroupId;
      Student.UpdatedAt = DateTime.Now;

      if (updateDto.Photo != null) {
        if (!string.IsNullOrEmpty(Student.Image))
          FileManager.Delete(_env.WebRootPath, "images/students", Student.Image);

        Student.Image = FileManager.Save(updateDto.Photo, _env.WebRootPath, "images/students");
      }

      await _StudentRepository.SaveAsync();
    }

    public async Task Delete(int id) {
      var Student = await _StudentRepository.GetAsync(x => x.Id == id && !x.IsDeleted)
        ?? throw new RestException(StatusCodes.Status404NotFound, "Student not found");

      Student.IsDeleted = true;
      Student.UpdatedAt = DateTime.Now;

      await _StudentRepository.SaveAsync();
    }
  }
}
