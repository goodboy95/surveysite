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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="quizId"></param>
        /// <param name="quizName"></param>
        /// <param name="quizIntro"></param>
        /// <param name="quizJson"></param>
        /// <returns></returns>
        [HttpPost("quiz")]
        public ActionResult SaveQuiz([FromForm]int quizId, [FromForm]string quizName, [FromForm]string quizIntro, [FromForm]string quizJson)
        {
            var quizObj = new QuizEntity();
            int.TryParse(Request.Cookies["id"], out int creator);
            quizObj.QuizBody = quizJson;
            quizObj.QuizName = quizName;
            quizObj.QuizIntro = quizIntro;
            quizObj.QuizCreator = creator;
            quizObj.QuizLikes = new List<string>();
            dbc.Quiz.Add(quizObj);
            if (quizId > 0)
            {
                var prevQues = dbc.Quiz.Find(quizId);
                prevQues.QuizIsDeleted = true;
                dbc.Quiz.Update(prevQues);
            }
            dbc.SaveChanges();
            return JsonReturn.ReturnSuccess();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="quizID"></param>
        /// <param name="answer"></param>
        /// <returns></returns>
        [HttpPost("answer")]
        public ActionResult SaveAnswer([FromForm]int quizID, [FromForm]string answer)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="quizID"></param>
        /// <returns></returns>
        [HttpGet("quiz")]
        public ActionResult GetQuiz([FromQuery]int quizID)
        {
            return JsonReturn.ReturnSuccess(dbc.Quiz.Find(quizID));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="answerID"></param>
        /// <returns></returns>
        [HttpGet("answer")]
        public ActionResult GetAnswer([FromQuery]int answerID)
        {
            var answerEntity = dbc.Answer.Find(answerID);
            var relQuizID = answerEntity.QuizID;
            var answerBody = answerEntity.AnswerBody;
            var quizBody = dbc.Quiz.Find(relQuizID).QuizBody;
            var result = new JObject(){ ["quizBody"] = quizBody, ["answerBody"] = answerBody };
            return JsonReturn.ReturnSuccess(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("quiz_list")]
        public ActionResult GetQuizList()
        {
            var quizList = from al in dbc.Quiz where al.QuizIsDeleted == false select al;
            return JsonReturn.ReturnSuccess(quizList);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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