using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskTracker.Models.Exceptions;

namespace TaskTracker.Models
{
    /// <summary>
    /// Модель задачи
    /// </summary>
    public class TaskModel
    {
        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        public int TaskId { get; set; }
        /// <summary>
        /// Наименование задачи
        /// </summary>
        [Required(ErrorMessage = "Введите название задачи.")]
        public string Name { get; set; }
        /// <summary>
        /// Описание задачи
        /// </summary>
        [Required(ErrorMessage = "Введите описание задачи.")]
        public string Description { get; set; }
        /// <summary>
        /// Список исполнителей
        /// </summary>
        [Required(ErrorMessage = "Назначьте как минимум одного исполнителя.")]
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
        [Column(TypeName = "decimal(6,2)")]
        [Required(ErrorMessage = "Введите плановое время выполнения задачи.")]
        public decimal EstimatedTime { get; set; }
        /// <summary>
        /// Фактическое время выполнения
        /// </summary>
        [Column(TypeName = "decimal(6,2)")]
        public decimal? CompletedTime { get; set; }
        /// <summary>
        /// Дата завершения задачи
        /// </summary>
        public DateTime? DateFinish { get; set; } = null;

        private int? _parentId;

        /// <summary>
        /// Идентификатор родительской задачи
        /// </summary>
        public int? ParentId
        {
            get { return _parentId; }
            set 
            {
                if (value == TaskId)
                {
                    throw new ServerException("Нельзя установить задачу подзадачей самой себе");
                }
                _parentId = value;
            }
        }

        public TaskModel? ParentTask { get; set; }
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
