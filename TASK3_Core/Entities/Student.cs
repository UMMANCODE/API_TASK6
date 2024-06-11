namespace TASK3_Core.Entities {
  public class Student : AuditEntity {
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public DateTime? BirthDate { get; set; }
    public int GroupId { get; set; }
    public Group? Group { get; set; }
    public string? Image { get; set; }
  }
}
