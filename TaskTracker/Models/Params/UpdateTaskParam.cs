using System.ComponentModel.DataAnnotations;
using TaskTracker.Models.Enums;

namespace TaskTracker.Models.Params
{
    /// <summary>
    /// Параметр редактирования задачи
    /// </summary>
    public class UpdateTaskParam
    {
        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        [Required(ErrorMessage = "Укажите идентификатор редактируемой задачи")]
        public int TaskId { get; set; }
        /// <summary>
        /// Наименование задачи
        /// </summary>
        [MaxLength(100)]
        public string? Name { get; set; }
        /// <summary>
        /// Описание задачи
        /// </summary>
        [MaxLength(100)]
        public string? Description { get; set; }
        /// <summary>
        /// Список исполнителей
        /// </summary>
        [MaxLength(100)]
        public string? Executor { get; set; }
        /// <summary>
        /// Статус задачи
        /// </summary>
        [EnumDataType(typeof(TaskStatusEnum))]
        public TaskStatusEnum? Status { get; set; }
        /// <summary>
        /// Плановая трудоемкость задачи
        /// </summary>
        public decimal? EstimatedTime { get; set; }
        /// <summary>
        /// Фактическое время выполнения
        /// </summary>
        public decimal? CompletedTime { get; set; }
        /// <summary>
        /// Дата завершения задачи
        /// </summary>
        public DateTime? DateFinish { get; set; } = null;
        /// <summary>
        /// Идентификатор родительской задачи
        /// </summary>
        [Range(1, int.MaxValue)]
        public int? ParentId { get; set; }
    }
}