using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AspNetCore_Docker.Models;
using PostgreDB_Connection;
using PostgreDB_Connection.Entities;

namespace AspNetCore_Docker.Controllers
{
    public class HomeController : Controller
    {
        private static PostgreSQL_Context context = new PostgreSQL_Context();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (TempData["DataDB"] != null)
            {
                ViewBag.DataDB = TempData["DataDB"].ToString();
            }
            return View();
        }

        public IActionResult Add_Users()
        {
            string exito = "";
            users usr = new users();
            try
            {
                usr.name = "Test";

                context.users.Add(usr);
                context.SaveChanges();

                exito = "Usuario creado correctamente.";
            }
            catch (Exception e)
            {
                exito = "Error al crear el usuario.";
            }

            TempData["DataDB"] = exito;
            return RedirectToAction("Index");
        }

        public IActionResult Reload()
        {
            string exito = "";

            try
            {
                TempData["DataDB"] = String.Concat(context.users.Where(us => us.name == "Test").Select(usr => usr.name).FirstOrDefault(), ". Usuarios en BD: ", context.users.Where(us => us.name == "Test").ToList().Count().ToString());
                
            }
            catch (Exception e)
            {
                exito = "Error al listar usuarios: " + e.Message;
                TempData["DataDB"] = exito;
            }
            
            return RedirectToAction("Index");
        }

        public IActionResult Use_Seed()
        {
            string exito = "";
            try
            {
                PostgreSQL_Seeder.Seed(context);
                exito = "Seeder lanzado con éxito.";

            }
            catch (Exception e)
            {
                exito = "Error al lanzar el seeder.";
            }
            TempData["DataDB"] = exito;
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
