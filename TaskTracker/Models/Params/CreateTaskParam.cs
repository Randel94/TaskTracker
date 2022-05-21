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
        /// Статус задачи
        /// </summary>
        public TaskStatusEnum Status { get; set; }
        /// <summary>
        /// Плановая трудоемкость задачи
        /// </summary>
        [Required(ErrorMessage = "Введите плановое время выполнения задачи.")]
        public decimal EstimatedTime { get; set; }
        /// <summary>
        /// Идентификатор родительской задачи
        /// </summary>
        public int? PatentId { get; set; }
    }
}
