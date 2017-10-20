using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dao;
using Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Domain.Entity;

namespace web.Api.Controllers
{
    [Route("[controller]")]
    public class HomeApiController : ApiBaseController
    {
        public HomeApiController(DwDbContext dbc, ILoggerFactory logFac, IServiceProvider svp) : base(dbc, logFac, svp)
        {
        }
        [HttpPost("create_survey")]
        public ActionResult CreateSurvey(string surveyJson)
        {
            var surveyJarr = JArray.Parse(surveyJson);
            var surveyObj = new SurveyEntity();
            surveyObj.SurveyBody = surveyJson;
            surveyObj.SurveyName = "test";
            dbc.Survey.Add(surveyObj);
            dbc.SaveChanges();
            return JsonReturn.ReturnSuccess(surveyJarr);
        }
        [HttpGet("survey_ques")]
        public ActionResult GetSurveyQues(int surveyID)
        {
            var surveyBody = dbc.Survey.Find(surveyID).SurveyBody;
            return JsonReturn.ReturnSuccess(surveyBody);
        }
    }
}