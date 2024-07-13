using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TaskManager.Models;
using TaskManager.Models.ViewModels;

namespace TaskManager.Controllers
{
    public class TaskController : Controller
    {
        private readonly HttpClient httpClient;
        private readonly IWebHostEnvironment env;
        dynamic students;

        public TaskController(HttpClient httpClient, IWebHostEnvironment env)
        {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri("https://localhost:44382/");
            this.env = env;
        }

        public async Task<IActionResult> Index()
        {
            var data = await GetStudentAsync(1);
            return View(data);
        }

        public async Task<IActionResult> AssignTask()
        {
            var response = await httpClient.GetAsync("api/Batches");

            if (response.IsSuccessStatusCode)
            {
                var batches = JsonConvert.DeserializeObject<List<Batch>>(await response.Content.ReadAsStringAsync());
                ViewBag.batches = batches;
                return View();
            }
            return View();
        }

        public void UploadFile(IFormFile file, string path)
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
        }

        [HttpPost]
        public async Task<IActionResult> AssignTask(AssignTaskViewModel model)
        {
            var path = env.WebRootPath;
            var filePath = "Content/Images/" + model.Attachment.FileName; //placing the image
            var fullPath = Path.Combine(path, filePath);
            UploadFile(model.Attachment, fullPath);


            NewTaskDto task = new ()
            {
                AttachmentPath = filePath,
                TaskDescription = model.TaskDescription,
                TaskAssignmentsDto = [
                    new TaskAssignmentDto(){
                        BatchId=model.SelectedBatch,
                        Students = students
                    }
                ]
            };

            var response = await httpClient.PostAsJsonAsync("api/NewTasks", task);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index"); // Redirect to a list or detail view after successful creation
            }

            return RedirectToAction(nameof(AssignTask));
        }



        public async Task<JsonResult> GetStudentsByBatch(int batch)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"api/Batches/{batch}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var batchResponse = JsonConvert.DeserializeObject<BatchResponseDto>(responseBody);
            students = batchResponse.Students;

            return Json(students);
        }



        /*public async Task<JsonResult> GetStudentsByBatch(int batch)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"api/Batches/{batch}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return Json(JsonConvert.DeserializeObject<BatchResponseDto>(responseBody).Students);
        }*/


        [HttpGet]
        public async Task<JsonResult> GetUsersByBatch(string batch)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"api/User/GetUsersByBatch/{batch}");

            if (response.IsSuccessStatusCode)
            {
                var users = JsonConvert.DeserializeObject<List<UserDto>>(await response.Content.ReadAsStringAsync());
                return Json(users);
            }

            return Json(new List<UserDto>());
        }

        public async Task<StudentDto> GetStudentAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"api/Students/{id}");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() }
                };
                return System.Text.Json.JsonSerializer.Deserialize<StudentDto>(responseBody, options);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return null;
            }
        }
    }
}
