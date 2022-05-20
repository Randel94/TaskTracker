using Microsoft.EntityFrameworkCore;
using TaskTracker.Data;
using TaskTracker.Helpers;
using TaskTracker.Models;
using TaskTracker.Models.Exceptions;

namespace TaskTracker.Services.TaskServices
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _dbContext;

        public TaskService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TaskModel>> GetTaskList()
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

        public async Task<TaskModel> GetTask(int taskId)
        {
            var response = new TaskModel();

            try
            {
                response = await _dbContext.Task
                    .FirstOrDefaultAsync(x => x.TaskId == taskId);

                if (response == null)
                {
                    throw new ObjectNotFoundException("Задача с таким идентификатором не найдена.");
                }
            }
            catch (Exception ex)
            {
                throw new ServerException("Get Task exception : " + ex.Message);
            }

            return response;
        }

        public async Task<TaskModel> CreateTask(TaskModel task)
        {
            try
            {
                await _dbContext.AddAsync(task);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ServerException("Create Task exception: " + ex.Message);
            }

            return task;
        }

        public async Task<TaskModel> UpdateTask(TaskModel task)
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