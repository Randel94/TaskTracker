using System.ComponentModel.DataAnnotations;
using TaskTracker.Models.Enums;

namespace TaskTracker.Models.Params
{
    /// <summary>
    /// Параметр создания задачи
    /// </summary>
    public class CreateTaskParam
    {
        /// <summary>
        /// Наименование задачи
        /// </summary>
        [Required(ErrorMessage = "Введите название задачи.")]
        [MaxLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// Описание задачи
        /// </summary>
        [Required(ErrorMessage = "Введите описание задачи.")]
        [MaxLength(100)]
        public string Description { get; set; }
        /// <summary>
        /// Список исполнителей
        /// </summary>
        [Required(ErrorMessage = "Назначьте как минимум одного исполнителя.")]
        [MaxLength(100)]
        public string Executor { get; set; }
        /// <summary>
        /// Статус задачи
        /// </summary>
        [EnumDataType(typeof(TaskStatusEnum))]
        public TaskStatusEnum Status { get; set; }
        /// <summary>
        /// Плановая трудоемкость задачи
        /// </summary>
        [Required(ErrorMessage = "Введите плановое время выполнения задачи.")]
        public decimal EstimatedTime { get; set; }
        /// <summary>
        /// Идентификатор родительской задачи
        /// </summary>
        [Range(1, int.MaxValue)]
        public int? ParentId { get; set; }
    }
}
