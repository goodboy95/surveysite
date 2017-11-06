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
    public class QuizController : ViewBaseController
    {
        public QuizController(DwDbContext dbc, ILoggerFactory logFac, IServiceProvider svp) : base(dbc, logFac, svp) { }

        public IActionResult CreateQuestionnaire([FromRoute]int id)
        {
            ViewBag.quesGroupId = id;
            return View();
        }
        public IActionResult QuizPage([FromRoute]int id)
        {
            ViewBag.surveyID = id;
            return View();
        }
    }
}
