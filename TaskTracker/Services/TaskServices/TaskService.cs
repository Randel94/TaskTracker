using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Data;
using TaskTracker.Helpers;
using TaskTracker.Models.Entities;
using TaskTracker.Models.Enums;
using TaskTracker.Models.Exceptions;
using TaskTracker.Models.DTOs;
using TaskTracker.Models.Params;

namespace TaskTracker.Services.TaskServices
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public TaskService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<TaskEntity>> GetTaskList()
        {
            try
            {
                var taskList = await _dbContext.Task
                    .ToListAsync();

                return taskList;
            }
            catch (Exception ex)
            {
                throw new ServerException("Get Task List exception: " + ex.Message);
            }
        }

        public async Task<TaskDTO> GetTask(int taskId)
        {
            try
            {
                var task = await _dbContext.Task
                    .Include(x => x.ParentTask)
                    .Include(x => x.ChildTasks)
                    .FirstOrDefaultAsync(x => x.TaskId == taskId);

                if (task == null)
                {
                    throw new ObjectNotFoundException("Задача с таким идентификатором не найдена.");
                }

                return _mapper.Map<TaskDTO>(task);
            }
            catch (Exception ex)
            {
                throw new ServerException("Get Task exception : " + ex.Message);
            }
        }

        public async Task<TaskDTO> CreateTask(CreateTaskParam param)
        {
            try
            {
                var task = _mapper.Map<TaskEntity>(param);

                await _dbContext.AddAsync(task);

                await _dbContext.SaveChangesAsync();

                if (task.ParentId != null)
                {
                    task.ParentTask = await _dbContext.Task.FindAsync(task.ParentId);
                }

                return _mapper.Map<TaskDTO>(task);
            }
            catch (Exception ex)
            {
                throw new ServerException("Create Task exception: " + ex.Message);
            }
        }

        public async Task<TaskEntity> UpdateTask(TaskEntity task)
        {
            try
            {
                var oldTask = await _dbContext.Task
                    .FindAsync(task.TaskId);

                if (oldTask == null)
                {
                    throw new ObjectNotFoundException("Задача не найдена.");
                }

                oldTask.Name = task.Name;
                oldTask.Description = task.Description;
                oldTask.Executor = task.Executor;
                oldTask.Status = task.Status;
                oldTask.EstimatedTime = task.EstimatedTime;
                oldTask.CompletedTime = task.CompletedTime ?? oldTask.CompletedTime;
                oldTask.DateFinish = task.DateFinish ?? oldTask.DateFinish;
                oldTask.ParentId = task.ParentId ?? oldTask.ParentId;

                await _dbContext.SaveChangesAsync();

                return oldTask;
            }
            catch (Exception ex)
            {
                throw new ServerException("Update Task exception: " + ex.Message);
            }
        }

        public async Task Delete(int taskId)
        {
            try
            {
                var task = await _dbContext.Task
                    .FirstOrDefaultAsync(x => x.TaskId == taskId);

                _dbContext.Task.Remove(task);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ServerException("Delete Task exception: " + ex.Message);
            }
        }

        public async Task<Dictionary<int, string>> GetTaskStatusList()
        {
            try
            {
                var statuses = Enum.GetValues(typeof(TaskStatusEnum))
                    .Cast<TaskStatusEnum>()
                    .ToDictionary(k => (int)k, v => v.GetDescription());

                return statuses;
            }
            catch (Exception ex)
            {
                throw new ServerException("Get Task Status List exception: " + ex.Message);
            }
        }
    }
}