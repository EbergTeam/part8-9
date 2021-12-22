using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;
using WebApplication3.Service;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        // получение своего сервиса приложения через механизм dependency injection - Через конструктор
        ITimeService _timeService;
        public HomeController(ITimeService timeService)
        {
            _timeService = timeService;
        }
        public string GetTime()
        {
            return _timeService.Time;
        }

        // переопределение метода OnActionExecuting(). Выполняется при вызове метода контроллера до его непосредственного выполнения
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Query.FirstOrDefault(p => p.Key == "count").Value != "25")
                context.Result = Content("Запрещен доступ в клуб кому 25");
            base.OnActionExecuting(context);
        }
        // переопределение метода OnActionExecuted(). Выполняется после выполнения метода контроллера
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //context.Result = Content("До скорой встречи в клубе кому 26");
            base.OnActionExecuted(context);
        }
        // получение в контроллере различной информации, связанную с контекстом контроллера - свойство ControllerContext
        public void Info()
        {
            //return ControllerContext.HttpContext.Request.Path;
            string table = "";
            foreach (var header in Request.Headers)
            {
                table += $"<tr><td>{header.Key}</td><td>{header.Value}</td></tr>";
            }
            Response.WriteAsync($"<table>{table}</table>");
        }
        // Получение данных из строки запроса
        public string Index(int count)
        {      
            var name = Request.Query.FirstOrDefault(p => p.Key == "name").Value;
            return "Good job " + name + ". Count=" + count;
        }
        // IActionResult - генерация результата действия. Готовые шаблоны
        public IActionResult TestAction()
        {
            //return Content("asd");
            //return new EmptyResult(); // анологичен public void GetVoid() { }
            //return new ObjectResult(new { Name = "Василий", Age = 22 });
            //var jsonUser = new { Name = "Tomas", Skill = "azaza" };
            //return new JsonResult(jsonUser);        
            //return Redirect("/Home/Privacy"); // Redirect/RedirectPermanent - временная/постоянная переадресация
                                                // LocalRedirect/LocalRedirectPermanent - для обращения к локальным адресам 
            return RedirectToAction("Index", "Home"); // RedirectToActionResult/RedirectToActionPermanent - переадресация на определенный метод контроллера
                                                      // RedirectToRoute/RedirectToRoutePermanent - для переадресации использует маршруты
        }
        public IActionResult TestStatusCode()
        {
            //return StatusCode(401); // можно отправить любой статусный код, но для некоторых есть свои классы
            //return NotFound("Ресурс не найден"); // NotFoundResult/NotFoundObjectResult - код 404 
            //return Unauthorized("Авторизация не пройдена"); // UnauthorizedResult/UnauthorizedObjectResult - код 401 без параметров/c параметрами
            //return BadRequest("Некорректный запрос"); // BadResult/BadObjectResult - код 400 без параметров/c параметрами
            return Ok("Запрос выполнен успешно"); // OkResult/OkObjectResult - код 200 без параметров/c параметрами
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
