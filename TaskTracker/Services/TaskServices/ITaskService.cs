using TaskTracker.Models.Entities;
using TaskTracker.Models.DTOs;
using TaskTracker.Models.Params;

namespace TaskTracker.Services.TaskServices
{
    public interface ITaskService
    {
        /// <summary>
        /// Получение списка задач.
        /// </summary>
        /// <returns></returns>
        Task<List<TaskDTO>> GetTaskList();
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
        Task<TaskDTO> CreateTask(CreateTaskParam param);
        /// <summary>
        /// Редактирование задачи.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        Task<TaskDTO> UpdateTask(UpdateTaskParam task);
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
