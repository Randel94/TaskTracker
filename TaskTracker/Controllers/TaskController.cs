using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Data;
using TaskTracker.Models;
using TaskTracker.Services.TaskServices;

namespace TaskTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Получение списка задач.
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> GetTaskList()
        {
            var response = await _taskService.GetTaskList();

            return Ok(response);
        }

        /// <summary>
        /// Получение описания задачи по идентификатору.
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpGet("{taskId}")]
        public async Task<IActionResult> Get([FromRoute] int taskId)
        {
            var response = await _taskService.GetTask(taskId);

            return Ok(response);
        }

        /// <summary>
        /// Создание задачи.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskModel task)
        {
            var response = await _taskService.CreateTask(task);

            return Ok(response);
        }

        /// <summary>
        /// Редактирование задачи.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateTask([FromBody] TaskModel task)
        {
            var response = await _taskService.UpdateTask(task);

            return Ok(response);
        }

        /// <summary>
        /// Удаление задачи.
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            await _taskService.Delete(taskId);

            return Ok();
        }

        /// <summary>
        /// Получение списка статусов задач.
        /// </summary>
        /// <returns></returns>
        [HttpGet("statuses")]
        public async Task<IActionResult> GetStatuses()
        {
            var response = await _taskService.GetTaskStatusList();

            return Ok(response);
        }
    }
}
