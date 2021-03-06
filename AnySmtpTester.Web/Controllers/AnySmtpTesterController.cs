﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

            return Json(SmtpHelper.GetResponse(host, port, "EHLO"));
        }

        [HttpPost]
        public JsonResult Sendmail()
        {
            var smtpClient = new SmtpClient
                                 {
                                     Host = "smtp.ticoticabum.com.br",
                                     Port = 25,
                                     UseDefaultCredentials = false,
                                     Credentials = new NetworkCredential("vendas@ticoticabum.com.br", "tico5000"),
                                     EnableSsl = false
                                 };
            try
            {
                new AnySmtpTester(smtpClient).SendMessage("vendas@ticoticabum.com.br", "alan.soares@vtex.com.br", "teste", "teste message");

                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }


        }

    }
}
