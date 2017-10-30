using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web.Controllers;
using Dao;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace simpleproj.Controllers
{
    public class SurveyController : ViewBaseController
    {
        public SurveyController(DwDbContext dbc, ILoggerFactory logFac, IServiceProvider svp) : base(dbc, logFac, svp) { }

        // GET: /<controller>/
        public IActionResult CreateQuestionnaire() => View();
        public IActionResult Survey(int id)
        {
            ViewBag.surveyID = id;
            return View();
        }
    }
}
