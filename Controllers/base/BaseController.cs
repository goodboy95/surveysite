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
        protected virtual void LoginFail(ActionExecutingContext context) { }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            long userID = Convert.ToInt64(Request.Cookies["id"]);
            string token = Request.Cookies["token"];
            var curUser = (from u in dbc.User where u.UserID == userID select new { u.Token, u.ExpireTime }).FirstOrDefault();
            string realToken = curUser?.Token;
            var tokenTime = curUser?.ExpireTime;
            //if (wsa == null) { wsa = new WebSocketAccessor(context.HttpContext); }
            //if (wsa.webSocket?.State != WebSocketState.Open) { wsa.SocketOpen(); }
            if (curUser == null || token == null || realToken == null || token != realToken || tokenTime < DateTime.Now)
            {
                LoginFail(context);
            }
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}