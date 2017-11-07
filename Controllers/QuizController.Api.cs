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
using Newtonsoft.Json;

namespace web.Api.Controllers
{
    [Route("[controller]")]
    public class QuizApiController : ApiBaseController
    {

        public QuizApiController(DwDbContext dbc, ILoggerFactory logFac, IServiceProvider svp) : base(dbc, logFac, svp)
        {
        }

        [HttpPost("quiz")]
        public ActionResult SaveQuestionnire(int quesId, string quesName, string quesIntro, string quesJson)
        {
            var quizObj = new QuizEntity();
            int.TryParse(Request.Cookies["id"], out int creator);
            quizObj.QuizBody = quesJson;
            quizObj.QuizName = quesName;
            quizObj.QuizIntro = quesIntro;
            quizObj.QuizCreator = creator;
            quizObj.QuizLikes = new List<string>();
            dbc.Quiz.Add(quizObj);
            if (quesId > 0)
            {
                var prevQues = dbc.Quiz.Find(quesId);
                prevQues.QuizIsDeleted = true;
                dbc.Quiz.Add(prevQues);
            }
            dbc.SaveChanges();
            return JsonReturn.ReturnSuccess();
        }

        [HttpPost("answer")]
        public ActionResult SaveAnswer(int quizID, string answer)
        {
            var answerObj = new AnswerEntity();
            int.TryParse(Request.Cookies["id"], out int creator);  //暂时没有用户名
            answerObj.AnswerCreator = creator;
            answerObj.AnswerIP = new HttpParser(HttpContext).GetIPAddr();
            answerObj.QuizID = quizID;
            answerObj.AnswerBody = answer;
            dbc.Answer.Add(answerObj);
            dbc.SaveChanges();
            return JsonReturn.ReturnSuccess();
        }

        [HttpGet("quiz")]
        public ActionResult GetQuestionnire(int quizID)
        {
            return JsonReturn.ReturnSuccess(dbc.Quiz.Find(quizID));
        }
        
        [HttpGet("answer")]
        public ActionResult GetAnswer(int answerID)
        {
            var answerEntity = dbc.Answer.Find(answerID);
            var relQuizID = answerEntity.QuizID;
            var answerBody = answerEntity.AnswerBody;
            var quizBody = dbc.Quiz.Find(relQuizID).QuizBody;
            var result = new JObject(){ ["quizBody"] = quizBody, ["answerBody"] = answerBody };
            return JsonReturn.ReturnSuccess(result);
        }

        [HttpGet("quiz_list")]
        public ActionResult GetQuizList()
        {
            var quizList = from al in dbc.Quiz where al.QuizIsDeleted == false select al;
            return JsonReturn.ReturnSuccess(quizList);
        }
        
        [HttpGet("answer_list")]
        public ActionResult GetAnswerList()
        {
            var quizList = from sl in dbc.Quiz where sl.QuizIsDeleted == false select sl;
            var answerList = from al in dbc.Answer where al.AnswerIsDeleted == false 
            join ql in quizList on al.QuizID equals ql.QuizID
            select new { AnswerID = al.AnswerID, AnswerBody = al.AnswerBody, AnswerIP = al.AnswerIP,
                QuizName = ql.QuizName, QuizBody = ql.QuizBody };
            return JsonReturn.ReturnSuccess(answerList);
        }
    }
}