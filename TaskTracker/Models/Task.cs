using System.ComponentModel;

namespace TaskTracker.Models
{
    /// <summary>
    /// Модель задачи
    /// </summary>
    public class Task
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
        public DateTime DateReg { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// Статус задачи
        /// </summary>
        public TaskStatus Status { get; set; }
        /// <summary>
        /// Плановая трудоемкость задачи
        /// </summary>
        public decimal EstimatedTime { get; set; }
        /// <summary>
        /// Фактическое время выполнения
        /// </summary>
        public decimal CompletedTime { get; set; }
        /// <summary>
        /// Дата завершения задачи
        /// </summary>
        public DateTime? DateFinish { get; set; } = null;
        /// <summary>
        /// Идентификатор родительской задачи
        /// </summary>
        public int? ParentId { get; set; }
        public Task ParentTask { get; set; }
    }

    public enum TaskStatus
    {
        [Description("Назначена")]
        Assigned,
        [Description("Выполняется")]
        Active,
        [Description("Приостановлена")]
        Suspended,
        [Description("Завершена")]
        Finished
    }
}
