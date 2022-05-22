using TaskTracker.Models.Enums;

namespace TaskTracker.Models.DTOs
{
    public class TaskDTO
    {
        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        public int TaskId { get; set; }
        /// <summary>
        /// Наименование задачи
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Описание задачи
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Список исполнителей
        /// </summary>
        public string Executor { get; set; }
        /// <summary>
        /// Дата регистрации задачи
        /// </summary>
        public DateTime DateReg { get; set; }
        /// <summary>
        /// Статус задачи
        /// </summary>
        public TaskStatusEnum Status { get; set; }
        /// <summary>
        /// Плановая трудоемкость задачи
        /// </summary>
        public decimal EstimatedTime { get; set; }
        /// <summary>
        /// Сумма плановой трудоемкости подзадач
        /// </summary>
        public decimal? EstimatedTimeChildSum
        {
            get
            {
                return ChildTasks?.Sum(x => x.EstimatedTimeChildSum + x.EstimatedTime);
            }
        }
        /// <summary>
        /// Фактическое время выполнения
        /// </summary>
        public decimal? CompletedTime { get; set; }
        /// <summary>
        /// Сумма фактического времени выполнения подзадач
        /// </summary>
        public decimal? CompletedTimeChildSum
        {
            get
            {
                return ChildTasks?.Sum(x => (x.CompletedTime ?? 0) + x.CompletedTimeChildSum);
            }
        }
        /// <summary>
        /// Дата завершения задачи
        /// </summary>
        public DateTime? DateFinish { get; set; }
        /// <summary>
        /// Родительская задача
        /// </summary>
        public TaskDTO? ParentTask { get; set; }
        /// <summary>
        /// Список подзадач
        /// </summary>
        public List<TaskDTO>? ChildTasks { get; set; }
    }
}
