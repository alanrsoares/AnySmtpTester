﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnySmtpTester.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "AnySMTP Tester";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}