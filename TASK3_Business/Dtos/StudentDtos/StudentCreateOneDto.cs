using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;

namespace TASK3_Business.Dtos.StudentDtos {
  public class StudentCreateOneDto {
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public DateTime? BirthDate { get; set; }
    public int GroupId { get; set; }
    public IFormFile? Photo { get; set; }
  }
}
