using TaskTracker.Models;

namespace TaskTracker.Services.TaskServices
{
    public interface ITaskService
    {
        /// <summary>
        /// Получение списка задач.
        /// </summary>
        /// <returns></returns>
        Task<List<TaskModel>> GetTaskList();
        /// <summary>
        /// Получение описания задачи по идентификатору.
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        Task<TaskModel> GetTask(int taskId);
        /// <summary>
        /// Создание задачи.
        /// </summary>
        /// <returns></returns>
        Task<TaskModel> CreateTask(TaskModel task);
        /// <summary>
        /// Редактирование задачи.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        Task<TaskModel> UpdateTask(TaskModel task);
        /// <summary>
        /// Удаление задачи.
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        Task Delete(int taskId);
        /// <summary>
        /// Получение списка статусов задач.
        /// </summary>
        /// <returns></returns>
        Task GetTaskStatusList();
    }
}
