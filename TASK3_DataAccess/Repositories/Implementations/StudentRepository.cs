using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TASK3_Core.Entities;
using TASK3_DataAccess.Repositories.Interfaces;

namespace TASK3_DataAccess.Repositories.Implementations {
  public class StudentRepository : Repository<Student>, IStudentRepository {

    public StudentRepository(AppDbContext context) : base(context) { }
  }
}
