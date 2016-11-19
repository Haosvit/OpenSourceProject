﻿using OpenSourceProject.OpenSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpenSourceProject.Controllers
{
	public class HomeController : Controller
	{
        private VerifyDbContext context = null;
		public ActionResult Index()
		{
            context = new VerifyDbContext();

			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

        public ActionResult Camera()
        {

            return View();
        }
    }
}