using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK3_Core.Entities;
using TASK3_DataAccess.Repositories.Interfaces;

namespace TASK3_DataAccess.Repositories.Implementations {
  public class GroupRepository : Repository<Group>, IGroupRepository {

    public GroupRepository(AppDbContext context) : base(context) { }
  }
}
