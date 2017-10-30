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
    public class QuizApiController : ApiBaseController
    {

        public QuizApiController(DwDbContext dbc, ILoggerFactory logFac, IServiceProvider svp) : base(dbc, logFac, svp)
        {
        }

        [HttpPost("questionnaire")]
        public ActionResult SaveQuestionnire(int quesId, string quesName, string quesIntro, string quesJson)
        {
            var surveyObj = new SurveyEntity();
            int.TryParse(Request.Cookies["id"], out int creator);
            surveyObj.SurveyBody = quesJson;
            surveyObj.SurveyName = quesName;
            surveyObj.SurveyIntro = quesIntro;
            surveyObj.SurveyCreator = creator;
            surveyObj.SurveyLikes = new List<string>();
            dbc.Survey.Add(surveyObj);
            if (quesId > 0)
            {
                var prevQues = dbc.Survey.Find(quesId);
                prevQues.SurveyIsDeleted = true;
                dbc.Survey.Add(prevQues);
            }
            dbc.SaveChanges();
            return JsonReturn.ReturnSuccess();
        }

        [HttpPost("answer")]
        public ActionResult SaveAnswer(int surveyID, string answer)
        {
            var answerObj = new AnswerEntity();
            int.TryParse(Request.Cookies["id"], out int creator);  //暂时没有用户名
            answerObj.AnswerCreator = creator;
            answerObj.AnswerIP = new HttpParser(HttpContext).GetIPAddr();
            answerObj.SurveyID = surveyID;
            answerObj.AnswerBody = answer;
            dbc.Answer.Add(answerObj);
            dbc.SaveChanges();
            return JsonReturn.ReturnSuccess();
        }

        [HttpGet("questionnaire")]
        public ActionResult GetQuestionnire(int surveyID)
        {
            var surveyBody = dbc.Survey.Find(surveyID).SurveyBody;
            var surveyJArr = JArray.Parse(surveyBody);
            return JsonReturn.ReturnSuccess(surveyJArr);
        }
        
        [HttpGet("answer")]
        public ActionResult GetAnswer(int answerID)
        {
            var answerEntity = dbc.Answer.Find(answerID);
            var relSurveyID = answerEntity.SurveyID;
            var answerBody = answerEntity.AnswerBody;
            var surveyBody = dbc.Survey.Find(relSurveyID).SurveyBody;
            var result = new JObject(){ ["surveyBody"] = surveyBody, ["answerBody"] = answerBody };
            return JsonReturn.ReturnSuccess(result);
        }

        [HttpGet("questionnaire_list")]
        public ActionResult GetQuestionnaireList()
        {
            var surveyList = from al in dbc.Survey where al.SurveyIsDeleted == false select al;
            return JsonReturn.ReturnSuccess(surveyList);
        }
        
        [HttpGet("answer_list")]
        public ActionResult GetAnswerList()
        {
            var answerList = from al in dbc.Answer where al.AnswerIsDeleted == false select al;
            return JsonReturn.ReturnSuccess(answerList);
        }
    }
}