using AutoMapper;
using TaskTracker.Models.Entities;
using TaskTracker.Models.DTOs;
using TaskTracker.Models.Params;

namespace TaskTracker.Profiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskEntity, TaskDTO>()
                .ForMember(d => d.ParentTask, opt => opt.MapFrom(src => src.ParentTask))
                .ForMember(d => d.ChildTasks, opt => opt.MapFrom(src => src.ChildTasks));

            CreateMap<CreateTaskParam, TaskEntity>()
                .ForMember(d => d.ParentId, opt => opt.MapFrom(src => src.ParentId));
        }
    }
}
