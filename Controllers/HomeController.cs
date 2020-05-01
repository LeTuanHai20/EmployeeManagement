using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Controllers
{
    [Authorize(Roles ="Admin,Employee")]
    public class HomeController : Controller
    {
        private readonly IEmployeeRespository _employeeRespository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<HomeController> logger;
        public HomeController(IEmployeeRespository employeeRespository, IWebHostEnvironment webHostEnvironment,
            ILogger<HomeController> logger)
        {
            _employeeRespository = employeeRespository;
            _webHostEnvironment = webHostEnvironment;
            this.logger = logger;
        }
         [AllowAnonymous]
        public IActionResult Index(int? id)
        {
            ViewBag.page = id;
            var model = _employeeRespository.GetAllEmployee();
            return View(model);
        }
        [AllowAnonymous]
        public IActionResult Details(int? id)
        {
            //throw new Exception("Error in Details Views");
            logger.LogTrace("Log Track");
            logger.LogDebug("Log Debug");
            logger.LogWarning("Log Waring");
            logger.LogError(" lOG ERROR");
            logger.LogCritical(" LogCritical ");
            EmployeeModel employee = _employeeRespository.GetEmployee(id.Value);
            if(employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }
            HomeDetailViewModel homeDetailViewModel = new HomeDetailViewModel()
            {
                Employee = employee,
                PageTitle = "Employee Detail"
            };
            return View(homeDetailViewModel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        private string ProcessUploadedFile(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadFile = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + model.Photo.FileName;
                string filePath = Path.Combine(uploadFile, uniqueFileName);
                using(var fileStream = new FileStream(filePath,FileMode.Create))
                model.Photo.CopyTo(fileStream);
            }
            return uniqueFileName;
        }
        [HttpPost]

        
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);
                EmployeeModel newEmployee = new EmployeeModel()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };

                _employeeRespository.Add(newEmployee);
                return RedirectToAction("details", new { id = newEmployee.ID });     
            }
            return View("Create");
        }
        [HttpGet]

      
        public IActionResult Edit(int id)
        {
            EmployeeModel employee = _employeeRespository.GetEmployee(id);
            HomeEditViewModel homeEditViewModel = new HomeEditViewModel()
            {
                ID = employee.ID,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };
            
            return View(homeEditViewModel);
        }
        [HttpPost]
        public IActionResult Edit(HomeEditViewModel model)
        {
           
            if (ModelState.IsValid) {
                EmployeeModel employee = _employeeRespository.GetEmployee(model.ID);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                if (model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    employee.PhotoPath = ProcessUploadedFile(model);
                    
                }
                else
                {
                    employee.PhotoPath = model.ExistingPhotoPath;
                }

                _employeeRespository.Update(employee);

                return RedirectToAction("details",new { id = employee.ID});
            }
             return View();      
          
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _employeeRespository.Delete(id);
            return RedirectToAction("index", "home");
        }
    }
}