using Microsoft.AspNetCore.Mvc;
using TASK3_Business.Dtos.StudentDtos;
using TASK3_Business.Services.Interfaces;
using TASK3_Core.Entities;
using TASK3_DataAccess;

namespace TASK3_Api.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class StudentController : ControllerBase {
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService) {
      _studentService = studentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 1) {
      var data = await _studentService.GetAll(pageNumber, pageSize);
      return StatusCode(200, data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      var data = await _studentService.GetById(id);
      return StatusCode(200, data);
    }

    [HttpPost("")]
    public async Task<IActionResult> Create(StudentCreateOneDto studentCreateOneDto) {
      int id = await _studentService.Create(studentCreateOneDto);
      return StatusCode(201, new { id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, StudentUpdateOneDto studentUpdateOneDto) {
      await _studentService.Update(id, studentUpdateOneDto);
      return StatusCode(204);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      await _studentService.Delete(id);
      return StatusCode(204);
    }
  }
}
