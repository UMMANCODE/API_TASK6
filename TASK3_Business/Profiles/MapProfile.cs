using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using TASK3_Core.Entities;
using AutoMapper;
using TASK3_Business.Dtos.GroupDtos;
using TASK3_Business.Dtos.StudentDtos;

namespace TASK3_Business.Profiles {
  public class MapProfile : Profile {
    private readonly IHttpContextAccessor _accessor;

    public MapProfile(IHttpContextAccessor accessor) {
      _accessor = accessor;

      var context = _accessor.HttpContext;

      var uriBuilder = new UriBuilder(context!.Request.Scheme, context.Request.Host.Host, context.Request.Host.Port ?? -1);
      if (uriBuilder.Uri.IsDefaultPort) {
        uriBuilder.Port = -1;
      }
      string baseUrl = uriBuilder.Uri.AbsoluteUri;

      CreateMap<Group, GroupGetOneDto>();
      CreateMap<Group, GroupGetAllDto>();
      CreateMap<GroupCreateOneDto, Group>();
      CreateMap<GroupUpdateOneDto, Group>();


      CreateMap<StudentCreateOneDto, Student>();
      CreateMap<StudentUpdateOneDto, Student>();
      CreateMap<Student, StudentGetOneDto>()
        .ForMember(d => d.ImageUrl, s => s.MapFrom(s => baseUrl + "images/students/" + s.Image));
      CreateMap<Student, StudentGetAllDto>()
        .ForMember(d => d.ImageUrl, s => s.MapFrom(s => baseUrl + "images/students/" + s.Image));
    }
  }
}
