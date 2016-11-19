using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpenSourceProject.Controllers
{
	public class CameraController : Controller
	{
		//
		// GET: /Camera/
		public ActionResult Index()
		{
			return PartialView("Index");
		}
	}
}