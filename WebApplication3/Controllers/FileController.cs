using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Controllers
{
    public class FileController : Controller
    {
        // получение встроенного сервиса приложения IWebHostEnvironment через механизм dependency injection - Через конструктор
        private readonly IWebHostEnvironment _appEnvironment;
        public FileController(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }
        // Загрузка файла по пути PhysicalFileResult
        public IActionResult GetFile()
        {
            // Путь к файлу
            string file_path = Path.Combine(_appEnvironment.ContentRootPath, "Files/book.pdf");
            // Тип файла - content-type
            string file_type = "application/pdf";
            // Имя файла - необязательно
            string file_name = "book.pdf";
            return PhysicalFile(file_path, file_type, file_name);
        }
        // Загрузка массива байтов
        public FileResult GetBytes()
        {
            string path = Path.Combine(_appEnvironment.ContentRootPath, "Files/book.pdf");
            byte[] mas = System.IO.File.ReadAllBytes(path);
            string file_type = "application/pdf";
            string file_name = "book2.pdf";
            return File(mas, file_type, file_name);
        }
        // Отправка потока
        public FileResult GetStream()
        {
            string path = Path.Combine(_appEnvironment.ContentRootPath, "Files/book.pdf");
            FileStream fs = new FileStream(path, FileMode.Open);
            string file_type = "application/pdf";
            string file_name = "book3.pdf";
            return File(fs, file_type, file_name);
        }
        public VirtualFileResult GetVirtualFile()
        {
            var filepath = Path.Combine("~/Files", "hello.txt"); // по умолчанию все пути к файлам будут сопоставляться с папкой wwwroot
            return File(filepath, "text/plain", "hello.txt");
        }
    }
}
