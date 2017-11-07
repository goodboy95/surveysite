using System;
using web.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dao;
using Utils;
using Domain.Entity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Cryptography;
using System.Text;

namespace web.Api.Controllers
{
    public abstract class ApiBaseController : BaseController
    {
        public ApiBaseController(DwDbContext dbc, ILoggerFactory logFac, IServiceProvider svp) : base(dbc, logFac, svp)
        {
        }
        protected override void LoginFail(ActionExecutingContext context)
        {
            context.Result = JsonReturn.ReturnFail(-3, "Illegal API access!");
        }
        
    }
}