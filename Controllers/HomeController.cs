using System;
using Microsoft.AspNetCore.Mvc;
using Dao;
using Utils;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using web.Api.Controllers;
using Newtonsoft.Json.Linq;
using System.Linq;
using Domain.Entity;

namespace web.Controllers
{
    public class HomeController : ViewBaseController
    {
        protected readonly HomeApiController homeApi;
        public HomeController(DwDbContext dbc, ILoggerFactory logFac, IServiceProvider svp) : base(dbc, logFac, svp)
        {
            homeApi = new HomeApiController(dbc, logFac, svp);
        }
        public IActionResult Index() 
        {
            ViewBag.CurIP = new HttpParser(HttpContext).GetIPAddr();
            var quesData = homeApi.GetQuestionnaireList();
            var quesList = ((quesData as JsonReturn)?.Data ?? new JArray()) as IQueryable<SurveyEntity>;
            var quesHtml = quesList.Select(q => $"<tr><th>{q.SurveyID}</th><th>{q.SurveyName}</th><th>{q.SurveyCreator}</th></tr>");
            var resultHtml = string.Join("<br />", quesHtml);
            ViewBag.QuesList = resultHtml;
            return View();
        }
        public IActionResult CreateQuestionnaire() => View();
        public IActionResult Survey(int id)
        {
            ViewBag.surveyID = id;
            return View();
        }
        public IActionResult Admin() => View();
        public IActionResult Logout()
        {
            var domain = new HttpParser(HttpContext).GetDomain();
            Response.Cookies.Append("id", "", new CookieOptions {Domain = domain, Expires = DateTime.Now.AddDays(-1.0)});
            Response.Cookies.Append("token", "", new CookieOptions {Domain = domain, Expires = DateTime.Now.AddDays(-1.0)});
            Response.Cookies.Append("username", "", new CookieOptions {Domain = domain, Expires = DateTime.Now.AddDays(-1.0)});
            return Redirect("/");
        }

    }
}
