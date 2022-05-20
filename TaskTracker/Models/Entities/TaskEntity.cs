using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskTracker.Models.Enums;
using TaskTracker.Models.Exceptions;

namespace TaskTracker.Models.Entities
{
    /// <summary>
    /// Модель задачи
    /// </summary>
    public class TaskEntity
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
        public TaskStatusEnum Status { get; set; }
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
                    throw new ForbiddenException("Нельзя установить задачу подзадачей самой себе");
                }
                _parentId = value;
            }
        }

        public TaskEntity? ParentTask { get; set; }
        public ICollection<TaskEntity>? ChildTasks { get; set; }
    }
}
