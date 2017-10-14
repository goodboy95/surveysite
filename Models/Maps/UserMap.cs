using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using Domain.Entity;
using System;
using Newtonsoft.Json.Linq;

namespace Domain.Mapping
{
    public static class UserMap
    {
        public static void MapUser(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<UserEntity>();

            entity.ToTable("userMessage");
            entity.HasKey(p => p.UserID);
            entity.Property(p => p.UserID).HasColumnName("user_id");
            entity.Property(p => p.UserCreateTime).HasColumnName("user_create_time");
            entity.Property(p => p.UserIsDeleted).HasColumnName("user_is_deleted");
            entity.Property(p => p.Name).HasColumnName("name").HasColumnType("varchar(30)").IsRequired();
            entity.Property(p => p.UserIP).HasColumnName("user_ip").HasColumnType("varchar(80)").IsRequired();
            entity.Property(p => p.Message).HasColumnName("message").HasColumnType("varchar(800)").IsRequired();
        }
    }
}
