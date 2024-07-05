using Dapper;
using is_takip_uygulamasi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Hosting;

namespace is_takip_uygulamasi.Controllers
{
    public class AdminController : Controller
    {
        string connectionString = "Server=X;Initial Catalog=X;User Id =X; Password =X;TrustServerCertificate=X";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(TaskModel model)
        {

            model.CreateTime = DateTime.Now;


            using var connection = new SqlConnection(connectionString);
            var sql = "INSERT INTO isTakip (name, detail, statusId, userId) VALUES (@Name, @Detail, @StatusId, @UserId)";

            var data = new
            {
                Name = model.Name,
                Detail = model.Detail,
                statusId = model.StatusId,
                userId = model.UserId,
            };

            var rowsAffected = connection.Execute(sql, data);

            //TempData["Message"] = $"<div class=\"alert alert-success\" role=\"alert\">\r\n  İlanınız başarı ile eklendi!\r\n</div>\r\n";
            //return View();
            ViewBag.MessageCssClass = "alert-success";
            ViewBag.Message = "Eklendi.";
            return View("Message");
        }

        public IActionResult Message()
        {
            return View();
        }
        public IActionResult Delete(int id)
        {
            using var connection = new SqlConnection(connectionString);
            var sql = "DELETE FROM isTakip WHERE id = @Id";

            var rowsAffected = connection.Execute(sql, new { Id = id });

            ViewBag.Message = "Silindi.";
            ViewBag.MessageCssClass = "alert-success";
            return View("Message");
        }

        [HttpGet]
        public IActionResult Edit()
        {
            using var connection = new SqlConnection(connectionString);
            var tasks = connection.Query<TaskModel>("SELECT * FROM isTakip");

            return View(tasks);
        }
    }
}
