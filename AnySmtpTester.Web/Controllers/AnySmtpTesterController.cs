using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AnySmtpTester;
namespace AnySmtpTester.Web.Controllers
{
    public class AnySmtpTesterController : Controller
    {
        //
        // GET: /AnySmtpTester/

        public JsonResult Index()
        {
            var host = "smtp.ticoticabum.com.br";
            var port = 25;

            return Json(SmtpHelper.TestConnection(host, port));
        }

    }
}
