using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ToDo.MvcUI.Models;
using ToDo.MvcUI.Services;

namespace ToDo.MvcUI.Controllers
{
    public class TasksController : Controller
    {
        private readonly IApiService _apiService;

        public TasksController(IApiService apiService)
        {
            _apiService = apiService;
        }

        private bool CheckAuth()
        {
            if (string.IsNullOrEmpty(_apiService.GetToken()))
            {
                return false;
            }
            return true;
        }

        public async Task<IActionResult> Index(int? status, int? categoryId)
        {
            if (!CheckAuth())
                return RedirectToAction("Login", "Account");

            var endpoint = "tasks";
            
            if (status.HasValue || categoryId.HasValue)
            {
                var queryParams = new List<string>();
                if (status.HasValue)
                    queryParams.Add($"state={status.Value}");
                if (categoryId.HasValue)
                    queryParams.Add($"categoryId={categoryId.Value}");
                
                endpoint = $"tasks/filter?{string.Join("&", queryParams)}";
            }

            var tasks = await _apiService.GetAsync<List<TaskViewModel>>(endpoint);
            
            var categories = await _apiService.GetAsync<List<CategoryViewModel>>("categories") 
                ?? new List<CategoryViewModel>();

            
            if (tasks != null && categories.Any())
            {
                foreach (var task in tasks)
                {
                    var category = categories.FirstOrDefault(c => c.Id == task.CategoryId);
                    if (category != null)
                    {
                        task.CategoryName = category.Name;
                    }
                }
            }

            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            ViewBag.SelectedStatus = status;
            ViewBag.SelectedCategoryId = categoryId;

            return View(tasks ?? new List<TaskViewModel>());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (!CheckAuth())
                return RedirectToAction("Login", "Account");

            var categories = await _apiService.GetAsync<List<CategoryViewModel>>("categories") 
                ?? new List<CategoryViewModel>();
            
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskViewModel model)
        {
            if (!CheckAuth())
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
            {
                var categories = await _apiService.GetAsync<List<CategoryViewModel>>("categories") 
                    ?? new List<CategoryViewModel>();
                ViewBag.Categories = new SelectList(categories, "Id", "Name");
                return View(model);
            }

            await _apiService.PostAsync<object>("tasks", model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!CheckAuth())
                return RedirectToAction("Login", "Account");

            var task = await _apiService.GetAsync<TaskViewModel>($"tasks/{id}");
            
            if (task == null)
                return NotFound();

            var categories = await _apiService.GetAsync<List<CategoryViewModel>>("categories") 
                ?? new List<CategoryViewModel>();
            
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TaskViewModel model)
        {
            if (!CheckAuth())
                return RedirectToAction("Login", "Account");

            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                var categories = await _apiService.GetAsync<List<CategoryViewModel>>("categories") 
                    ?? new List<CategoryViewModel>();
                ViewBag.Categories = new SelectList(categories, "Id", "Name");
                return View(model);
            }

            await _apiService.PutAsync<object>($"tasks/{id}", model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (!CheckAuth())
                return RedirectToAction("Login", "Account");

            await _apiService.DeleteAsync($"tasks/{id}");
            return RedirectToAction("Index");
        }
    }
}
