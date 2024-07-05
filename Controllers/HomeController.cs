using Dapper;
using is_takip_uygulamasi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace is_takip_uygulamasi.Controllers
{
    public class HomeController : Controller
    {
        string connectionString = "Server=X;Initial Catalog=X;User Id =X; Password =X;TrustServerCertificate=X";

        public IActionResult Index()
        {
            using var connection = new SqlConnection(connectionString);
            var sql = "SELECT t.id, t.name, t.detail, sl.name AS status, tu.username, t.createTime\r\nFROM isTakip t\r\nLEFT JOIN task_users tu ON t.userId = tu.id\r\nLEFT JOIN status_list sl ON t.statusId = sl.id;\r\n";
            var tasks = connection.Query<TaskModel>(sql).ToList();
            return View(tasks);
        }

        public IActionResult Task(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var taskModel = new TaskModel();

            using var connection = new SqlConnection(connectionString);
            var sql = "SELECT * FROM isTakip WHERE id = @Id";

            var task = connection.QuerySingleOrDefault<TaskModel>(sql, new { id = id });

            return View(task);
        }
    }
}
