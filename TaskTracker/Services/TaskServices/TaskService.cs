using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Data;
using TaskTracker.Helpers;
using TaskTracker.Models.Entities;
using TaskTracker.Models.Enums;
using TaskTracker.Models.Exceptions;
using TaskTracker.Models.DTOs;
using TaskTracker.Models.Params;
using AutoMapper.QueryableExtensions;

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

        /// <inheritdoc/>
        public async Task<List<TaskDTO>> GetTaskList()
        {
            try
            {
                var taskList = await _dbContext.Task
                    .Where(x => x.ParentId == null)
                    .Include(x => x.ChildTasks)
                    .ToListAsync();

                taskList.Where(x => x.ChildTasks.Any())
                    .ToList()
                    .ForEach(async y => y = await GetTaskRecursively(y.TaskId));

                return _mapper.Map<List<TaskDTO>>(taskList);
            }
            catch (Exception ex)
            {
                throw new ServerException("Get Task List exception: " + ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<TaskDTO> GetTask(int taskId)
        {
            try
            {
                var task = await GetTaskRecursively(taskId);

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

        /// <inheritdoc/>
        public async Task<TaskDTO> CreateTask(CreateTaskParam param)
        {
            try
            {
                var task = _mapper.Map<TaskEntity>(param);

                if (task.ParentId != null)
                {
                    var parentTask = await _dbContext.Task
                        .FindAsync(task.ParentId);
                    if (parentTask == null)
                    {
                        throw new ObjectNotFoundException("Не найдена родительская задача");
                    }
                }

                await _dbContext.AddAsync(task);

                await _dbContext.SaveChangesAsync();

                return _mapper.Map<TaskDTO>(task);
            }
            catch (Exception ex)
            {
                throw new ServerException("Create Task exception: " + ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<TaskDTO> UpdateTask(UpdateTaskParam task)
        {
            try
            {
                var oldTask = await GetTaskRecursively(task.TaskId);

                if (oldTask == null)
                {
                    throw new ObjectNotFoundException("Задача не найдена.");
                }

                oldTask.Name = task.Name ?? oldTask.Name;
                oldTask.Description = task.Description ?? oldTask.Description;
                oldTask.Executor = task.Executor ?? oldTask.Executor;
                oldTask.EstimatedTime = task.EstimatedTime ?? oldTask.EstimatedTime;
                oldTask.CompletedTime = task.CompletedTime ?? oldTask.CompletedTime;
                oldTask.ParentId = task.ParentId ?? oldTask.ParentId;

                if (task.Status != null)
                {
                    UpdateState(oldTask, task.Status.Value);
                }

                await _dbContext.SaveChangesAsync();

                return _mapper.Map<TaskDTO>(oldTask);
            }
            catch (Exception ex)
            {
                throw new ServerException("Update Task exception: " + ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task Delete(int taskId)
        {
            try
            {
                var task = await _dbContext.Task
                    .Include(x => x.ChildTasks)
                    .FirstOrDefaultAsync(x => x.TaskId == taskId);

                if (task == null)
                {
                    throw new ObjectNotFoundException("Задача не найдена.");
                }
                else if (task.ChildTasks.Any())
                {
                    throw new ForbiddenException("Нельзя удалить задачу, содержащую подзадачи");
                }
                
                _dbContext.Task.Remove(task);
                
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ServerException("Delete Task exception: " + ex.Message);
            }
        }

        /// <inheritdoc/>
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

        private async Task<TaskEntity> GetTaskRecursively(int taskId)
        {
            var task = await _dbContext.Task
                .Include(x => x.ParentTask)
                .Include(x => x.ChildTasks)
                .FirstOrDefaultAsync(x => x.TaskId == taskId);

            if (task?.ChildTasks is not null)
            {
                var childTasks = task.ChildTasks.ToList();
                
                for (var i = 0; i < childTasks.Count; i++)
                {
                    childTasks[i] = await GetTaskRecursively(childTasks[i].TaskId);
                }
            }

            return task;
        }

        private void UpdateState(TaskEntity task, TaskStatusEnum value)
        {
            if (value == TaskStatusEnum.Finished && task.Status == TaskStatusEnum.Assigned)
            {
                throw new ForbiddenException($"Нельзя завершить задачу из статуса \"Назначена\"");
            }

            if (task.ChildTasks.Any())
            {
                task.ChildTasks.ToList().ForEach(x => UpdateState(x, value));
            }

            task.Status = value;

            if (value == TaskStatusEnum.Finished)
            {
                task.DateFinish = DateTime.UtcNow;
            }
        }
    }
}