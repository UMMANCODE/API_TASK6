using System;
using System.ComponentModel.DataAnnotations;
namespace TASK3_Core.Entities {
  public class Group : AuditEntity {
    public string? Name { get; set; }
    public int Limit { get; set; }
    public List<Student> Students { get; set; } = new();
  }
}

