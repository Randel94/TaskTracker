using System.ComponentModel;

namespace TaskTracker.Models.Enums
{
    /// <summary>
    /// Список статусов задач
    /// </summary>
    public enum TaskStatusEnum
    {
        /// <summary>
        /// Назначена
        /// </summary>
        [Description("Назначена")]
        Assigned,
        /// <summary>
        /// Выполняется
        /// </summary>
        [Description("Выполняется")]
        Active,
        /// <summary>
        /// Приостановлена
        /// </summary>
        [Description("Приостановлена")]
        Suspended,
        /// <summary>
        /// Завершена
        /// </summary>
        [Description("Завершена")]
        Finished
    }
}
