using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Domain.Entity
{
    public class UserEntity
    {
        public UserEntity()
        {
            UserCreateTime = DateTime.Now;
            UserIsDeleted = false;
        }
        public long UserID { get; set; }
        public DateTime UserCreateTime { get; set; }
        public bool UserIsDeleted { get; set; }
        public string Name { get; set; }
        public string UserIP { get; set; }
        public string Message { get; set; }
    }
}
