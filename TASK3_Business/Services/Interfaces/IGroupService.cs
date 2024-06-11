using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK3_Business.Dtos.GroupDtos;

namespace TASK3_Business.Services.Interfaces {
  public interface IGroupService {
    public Task<int> Create(GroupCreateOneDto createDto);
    public Task<List<GroupGetAllDto>> GetAll(int pageNumber = 1, int pageSize = 1);
    public Task<GroupGetOneDto> GetById(int id);
    public Task Update(int id, GroupUpdateOneDto updateDto);
    public Task Delete(int id);
  }
}
