using TaskTracker.Models.Entities;
using TaskTracker.Models.DTOs;

namespace TaskTracker.Services.TaskServices
{
    public interface ITaskService
    {
        /// <summary>
        /// Получение списка задач.
        /// </summary>
        /// <returns></returns>
        Task<List<TaskEntity>> GetTaskList();
        /// <summary>
        /// Получение описания задачи по идентификатору.
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        Task<TaskDTO> GetTask(int taskId);
        /// <summary>
        /// Создание задачи.
        /// </summary>
        /// <returns></returns>
        Task<TaskEntity> CreateTask(TaskEntity task);
        /// <summary>
        /// Редактирование задачи.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        Task<TaskEntity> UpdateTask(TaskEntity task);
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
        Task<Dictionary<int, string>> GetTaskStatusList();
    }
}
