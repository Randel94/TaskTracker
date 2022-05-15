using TaskTracker.Data;
using TaskTracker.Models;

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
            var response = new List<TaskModel>();

            return response;
        }

        public async Task<TaskModel> GetTask(int taskId)
        {
            var response = new TaskModel();

            return response;
        }

        public async Task<TaskModel> CreateTask(TaskModel task)
        {
            return task;
        }

        public async Task<TaskModel> UpdateTask(TaskModel task)
        {
            return task;
        }

        public async Task Delete(int taskId)
        {
        }
    }
}