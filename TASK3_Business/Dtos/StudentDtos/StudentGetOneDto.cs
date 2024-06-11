namespace TASK3_Business.Dtos.StudentDtos {
  public class StudentGetOneDto {
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public DateTime? BirthDate { get; set; }
    public int GroupId { get; set; }
    public string? ImageUrl { get; set; }
  }
}
