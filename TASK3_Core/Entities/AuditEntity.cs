namespace TASK3_Core.Entities {
  public class AuditEntity : BaseEntity {
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
  }
}
