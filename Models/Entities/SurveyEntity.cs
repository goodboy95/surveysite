using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Domain.Entity
{
    public class SurveyEntity
    {
        public SurveyEntity()
        {
            SurveyCreateTime = DateTime.Now;
            SurveyIsDeleted = false;
        }
        public int SurveyID { get; set; }
        public int SurveyCreator { get; set; }
        public DateTime SurveyCreateTime { get; set; }
        public bool SurveyIsDeleted { get; set; }
        public string SurveyName { get; set; }
        public string SurveyIntro { get; set; }
        public string SurveyPicPath { get; set; }
        public string SurveyBody { get; set; }
        internal string _surveyLikes { get; set; }
        public List<string> SurveyLikes 
        {
             get { return JsonConvert.DeserializeObject<List<string>>(_surveyLikes); }
             set { _surveyLikes = JsonConvert.SerializeObject(value); }
             }
    }
}