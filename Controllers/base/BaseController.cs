using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dao;
using Domain.Entity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Filters;
using Utils;
using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;
using System.Threading;

namespace web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly DwDbContext dbc;
        protected ILogger _log;
        protected long userID;
        //protected StackRedisHelper redis;
        public BaseController(DwDbContext dbc, ILoggerFactory logFac, IServiceProvider svp)
        {
            this.dbc = dbc;
            _log = logFac.CreateLogger("simpleproj");
            //redis = StackRedisHelper.Instance;
        }


        public string GetIPAddr()
        {
            var ip = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = HttpContext.Connection.RemoteIpAddress.ToString();
            }
            return ip;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}