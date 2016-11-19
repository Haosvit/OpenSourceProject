﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpenSourceProject.Controllers
{
	public class LoginController : Controller
	{
		private string IMAGE_NAME_KEY = "image_name";
		private string IMAGE_PATH_KEY = "image_path";
		private string IMAGE_DIR = "~/CapturedImages/";
		//
		// GET: /Login/
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Register()
		{
			return View();
		}

		public ActionResult Login(string email)
		{
			if (email == "abc@abc.com")
			{
				return RedirectToAction("Face", "Login");
			}
			return View("Index");
		}

		public ActionResult Face()
		{
			return View();
		}

		public JsonResult UploadPicture()
		{
			string path = "~/CapturedImages/" + Session["val"].ToString();

			return Json(path, JsonRequestBehavior.AllowGet);

		}

		public ActionResult Capture()
		{
			var stream = Request.InputStream;

			string dump;

			string path = "";

			using (var reader = new StreamReader(stream))
			{
				dump = reader.ReadToEnd();

				DateTime nm = DateTime.Now;

				string date = nm.ToString("yyyymmddMMss");


				path = Server.MapPath(IMAGE_DIR + "user_" + date + ".jpg");

				string dir = Directory.GetParent(path).FullName;
				//path = Path.Combine(dir, "user_" + date + ".jpg");
				if (!Directory.Exists(dir))
				{
					Directory.CreateDirectory(dir);
				}

				//System.IO.File.SetAttributes(path, FileAttributes.Normal);

				// TODO: chỗ này t thấy lấy cái byte này gửi lên validate luôn cũng được, khỏi cần lưu
				System.IO.File.WriteAllBytes(path, String_To_Bytes2(dump));

				ViewData[IMAGE_PATH_KEY] = "user_" + date + ".jpg";

				Session[IMAGE_NAME_KEY] = "user_" + date + ".jpg";
			}

			//TODO validate login here
						
			return RedirectToAction("CheckLogin");
		}

		public ActionResult CheckLogin()
		{


			return RedirectToAction("Index", "Home");
			return RedirectToAction("Index");
		}

		 private byte[] String_To_Bytes2(string strInput)
		{
			int numBytes = (strInput.Length) / 2;

			byte[] bytes = new byte[numBytes];

			for (int x = 0; x < numBytes; ++x)
			{
				bytes[x] = Convert.ToByte(strInput.Substring(x * 2, 2), 16);
			}

			return bytes;
		}
	}

	
}