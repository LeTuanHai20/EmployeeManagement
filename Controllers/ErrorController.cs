using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Controllers
{
    //su dung logging de hien thi cac exeption hay cac error tren debug, console,... khong hien thi cac loi tren view 
    // nham securrity well : dau tien inject ilogger
    public class ErrorController : Controller
    {
        private readonly ILogger<HomeController> logger;

        public ErrorController(ILogger<HomeController> logger)
        {
            this.logger = logger;
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHander(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
 
                switch (statusCode)
                {
                    case 404:
                        ViewBag.ErrorMessage = "Sorry, the resoure you requested couldn't be found";
                        logger.LogWarning($"Error 404 occured on {statusCodeResult.OriginalPath} " +
                            $"+ query : {statusCodeResult.OriginalQueryString}");
                        break;
                }

            return View("NotFound");

        }
        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            logger.LogError($"the path: {exceptionHandlerPathFeature.Path}  + : {exceptionHandlerPathFeature.Error} ");
            return View("Error");
        }
    }
}