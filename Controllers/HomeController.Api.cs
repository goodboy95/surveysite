using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dao;
using Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace web.Api.Controllers
{
    [Route("[controller]")]
    public class HomeApiController : ApiBaseController
    {
        public HomeApiController(DwDbContext dbc, ILoggerFactory logFac, IServiceProvider svp) : base(dbc, logFac, svp)
        {
        }
        public ActionResult CreateSurvey(string surveyJson)
        {
            return null;
        }
        public ActionResult GetSurveyQues(int surveyID)
        {
            var surveyBody = dbc.Survey.Find(surveyID).SurveyBody;
            return JsonReturn.ReturnSuccess(surveyBody);
        }
    }
}