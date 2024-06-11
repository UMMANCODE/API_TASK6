using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TASK3_Business.Dtos.GroupDtos;
using TASK3_Business.Exceptions;
using TASK3_Business.Services.Interfaces;
using TASK3_Core.Entities;
using TASK3_DataAccess;
using TASK3_DataAccess.Repositories.Interfaces;
using TASK3_Business.Profiles;
using AutoMapper;

namespace TASK3_Business.Services.Implementations {
  public class GroupService : IGroupService {
    private readonly IGroupRepository _groupRepository;
    private readonly IMapper _mapper;

    public GroupService(IGroupRepository groupRepository, IMapper mapper) {
      _groupRepository = groupRepository;
      _mapper = mapper;
    }

    public async Task<int> Create(GroupCreateOneDto dto) {
      if (await _groupRepository.ExistsAsync(x => x.Name == dto.Name && !x.IsDeleted))
        throw new RestException(StatusCodes.Status400BadRequest, "Name", "Dublicate Name values");

      var entity = _mapper.Map<Group>(dto);

      await _groupRepository.AddAsync(entity);
      await _groupRepository.SaveAsync();

      return entity.Id;
    }
    public async Task<List<GroupGetAllDto>> GetAll(int pageNumber = 1, int pageSize = 1) {
      if (pageNumber <= 0 || pageSize <= 0) {
        throw new RestException(StatusCodes.Status400BadRequest, "Invalid parameters for paging");
      }

      var groups = await _groupRepository.GetAllAsync(x => !x.IsDeleted, pageNumber, pageSize);
      return _mapper.Map<List<GroupGetAllDto>>(groups);
    }
    public async Task<GroupGetOneDto> GetById(int id) {
      var group = await _groupRepository.GetAsync(x => x.Id == id && !x.IsDeleted);

      return group == null
        ? throw new RestException(StatusCodes.Status404NotFound, "Group not found")
        : _mapper.Map<GroupGetOneDto>(group);
    }
    public async Task Update(int id, GroupUpdateOneDto updateDto) {
      var group = await _groupRepository.GetAsync(x => x.Id == id && !x.IsDeleted, "Students")
        ?? throw new RestException(StatusCodes.Status404NotFound, "Group not found");

      if (group.Name != updateDto.Name && await _groupRepository.ExistsAsync(x => x.Name == updateDto.Name && !x.IsDeleted))
        throw new RestException(StatusCodes.Status400BadRequest, "Name", "Dublicate Name values");

      if (group.Students.Count > updateDto.Limit)
        throw new RestException(StatusCodes.Status400BadRequest, "Limit", "Limit overflow");

      group.Name = updateDto.Name;
      group.Limit = updateDto.Limit;
      group.UpdatedAt = DateTime.Now;

      await _groupRepository.SaveAsync();
    }

    public async Task Delete(int id) {
      var group = await _groupRepository.GetAsync(x => x.Id == id && !x.IsDeleted, "Students")
        ?? throw new RestException(StatusCodes.Status404NotFound, "Group not found");

      if (group.Students.Count > 0)
        throw new RestException(StatusCodes.Status400BadRequest, "Group", "Group has students");

      group.IsDeleted = true;
      group.UpdatedAt = DateTime.Now;

      await _groupRepository.SaveAsync();
    }
  }
}
