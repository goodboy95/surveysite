using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using Domain.Entity;
using System;
using Newtonsoft.Json.Linq;

namespace Domain.Mapping
{
    public static class SurveyMap
    {
        public static void MapSurvey(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<SurveyEntity>();
            entity.ToTable("survey");
            entity.HasKey(p => p.SurveyID);
            entity.Property(p => p.SurveyID).HasColumnName("survey_id");
            entity.Property(p => p.SurveyCreateTime).HasColumnName("survey_create_time");
            entity.Property(p => p.SurveyCreator).HasColumnName("survey_creator");
            entity.Property(p => p.SurveyIsDeleted).HasColumnName("survey_is_deleted");
            entity.Property(p => p.SurveyName).HasColumnName("survey_name").HasColumnType("varchar(80)").IsRequired();
            entity.Property(p => p.SurveyIntro).HasColumnName("survey_intro").HasColumnType("varchar(255)");
            entity.Property(p => p.SurveyPicPath).HasColumnName("survey_picpath");
            entity.Property(p => p.SurveyBody).HasColumnName("survey_body").IsRequired();
            entity.Property(p => p._surveyLikes).HasColumnName("survey_likes").IsRequired();
            entity.Ignore(p => p.SurveyLikes);
        }
    }
}