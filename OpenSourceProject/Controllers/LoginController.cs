using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OpenSourceProject.OpenSource;
using Newtonsoft.Json.Linq;
using System.Text;
using Newtonsoft.Json;

namespace OpenSourceProject.Controllers
{
	public class LoginController : Controller
	{
		private string IMAGE_DIR = "~/CapturedImages/";
		private VerifyDbContext context = null;
		private string personId = "";
		private string personGroupId = "open_source_net1";
		//
		// GET: /Login/
		public ActionResult Index()
		{
			//context = new VerifyDbContext();
			//var userDa = new UserDa();
			//var addedUser = userDa.Add(new User { Email = "hao@hao.com", Gender = "male", Name = "Hao", PersonId= new Guid().ToString() });
			//var verificationDa = new VerificationDa();

			//verificationDa.Add(new Verification { PersistedFaceId = new Guid().ToString(), UserId = addedUser.Id });

			return View();
		}

		public ActionResult Register()
		{
			string IMAGE_DIR1 = "~/Content/Images/Register/";
			string path1 = Server.MapPath(IMAGE_DIR1 + "RegisterImage1.jpg");
			string path2 = Server.MapPath(IMAGE_DIR1 + "RegisterImage2.jpg");
			string path3 = Server.MapPath(IMAGE_DIR1 + "RegisterImage3.jpg");
			System.IO.File.Delete(path1);
			System.IO.File.Delete(path2);
			System.IO.File.Delete(path3);
			return View();
		}

		public ActionResult Face()
		{
			return View();
		}

		public void Capture()
		{
			var stream = Request.InputStream;

			string dump;

			string path = "";

			using (var reader = new StreamReader(stream))
			{
				dump = reader.ReadToEnd();

				path = Server.MapPath(IMAGE_DIR + "login.jpg");

				string dir = Directory.GetParent(path).FullName;
				//path = Path.Combine(dir, "user_" + date + ".jpg");
				if (!Directory.Exists(dir))
				{
					Directory.CreateDirectory(dir);
				}
				// Save file was catured
				System.IO.File.WriteAllBytes(path, String_To_Bytes2(dump));
			}
		}

		public ActionResult CheckEmail()
		{
			string email = Request.Form["email"];
			personId = getPersonId(email);
			if (personId != "error")
			{
				return RedirectToAction("Face", "Login");
			}
			else
			{
				Response.Write(string.Format("<script type='text/javascript'>alert('Not found this email')</script>"));
				return RedirectToAction("Index", "Login");
			}
		}

		public ActionResult CheckLogin()
		{
			if (getFaceId() != "error")
			{
				if (verifyLogin(getFaceId()))
				{
					return RedirectToAction("Index", "Home");
				}
				else
				{
					Response.Write("<script language='JavaScript'> alert('Please try again...'); </script>");
					return RedirectToAction("Index", "Login");
				}

			}
			else
			{
				Response.Write("<script language='JavaScript'> alert('Please try again...'); </script>");
				return RedirectToAction("Index", "Login");
			}
		}

		private string getPersonId(string email)
		{
			string error = "error";
			try
			{
				// Send request
				var personRequest = (HttpWebRequest)WebRequest.Create("https://api.projectoxford.ai/face/v1.0/persongroups/" + personGroupId + "/persons");
				personRequest.Method = "GET";
				personRequest.Host = "api.projectoxford.ai";
				personRequest.Headers.Add("Ocp-Apim-Subscription-Key", "1c056c36ece84f14a0619803ee4f0ceb");

				// Get response
				var personResponse = (HttpWebResponse)personRequest.GetResponse();
				if (personResponse.StatusDescription.ToString().ToUpper() == "OK")
				{
					var responseString = new StreamReader(personResponse.GetResponseStream()).ReadToEnd();
					string json = responseString.ToString();
					dynamic personArray = JsonConvert.DeserializeObject(json);
					foreach (var item in personArray)
					{
						if (item.name == email)
						{
							return item.personId;
						}
					}
					return error;
				}
				else
				{
					Response.Write("<script language='JavaScript'> alert('Fail connection...'); </script>");
					return error;
				}
			}
			catch (Exception e)
			{
				Console.Write(e.Message);
				return error;
			}
		}

		private string getFaceId()
		{
			try
			{
				// Check exists image?
				string path = Server.MapPath(IMAGE_DIR + "login.jpg");
				// Face detect Login image
				if (System.IO.File.Exists(path))
				{
					// Create request to detect login image
					var loginRequest = (HttpWebRequest)WebRequest.Create("https://api.projectoxford.ai/face/v1.0/detect?returnFaceId=true");
					var imgBinary = System.IO.File.ReadAllBytes(path);
					loginRequest.Method = "POST";
					loginRequest.ContentType = "application/octet-stream";
					loginRequest.ContentLength = imgBinary.Length;
					loginRequest.Host = "api.projectoxford.ai";
					loginRequest.Headers.Add("Ocp-Apim-Subscription-Key", "1c056c36ece84f14a0619803ee4f0ceb");
					// Send request
					using (var stream = loginRequest.GetRequestStream())
					{
						stream.Write(imgBinary, 0, imgBinary.Length);
					}

					// Get response to get faceId
					var loginResponse = (HttpWebResponse)loginRequest.GetResponse();
					if (loginResponse.StatusDescription.ToString().ToUpper() == "OK")
					{
						var responseString = new StreamReader(loginResponse.GetResponseStream()).ReadToEnd();
						string json = responseString.ToString();
						dynamic faceArray = JsonConvert.DeserializeObject(json);
						string faceId = faceArray[0].faceId;
						return faceId;
					}
					else
					{
						Response.Write("<script language='JavaScript'> alert('Fail connection...'); </script>");
						return "error";
					}
				}
				else
				{
					return "error";
				}
			}
			catch (Exception e)
			{
				Console.Write(e.Message);
				return "error";
			}
		}

		private Boolean verifyLogin(string faceIdParam)
		{
			try
			{
				// Create request with faceId
				var verifyRequest = (HttpWebRequest)WebRequest.Create("https://api.projectoxford.ai/face/v1.0/verify");
				var postData = "{\"faceId\":\"" + faceIdParam + "\",\"personId\":\"" + personId + "\",\"personGroupId\":\"" + personGroupId + "\"}";
				byte[] byteData = Encoding.UTF8.GetBytes(postData);
				verifyRequest.Method = "POST";
				verifyRequest.ContentType = "application/json";
				verifyRequest.ContentLength = byteData.Length;
				verifyRequest.Host = "api.projectoxford.ai";
				verifyRequest.Headers.Add("Ocp-Apim-Subscription-Key", "1c056c36ece84f14a0619803ee4f0ceb");
				using (var stream = verifyRequest.GetRequestStream())
				{
					stream.Write(byteData, 0, byteData.Length);
				}
				// Get response
				var verifyResponse = (HttpWebResponse)verifyRequest.GetResponse();
				var responseString = new StreamReader(verifyResponse.GetResponseStream()).ReadToEnd();
				string json = responseString.ToString();
				dynamic result = JsonConvert.DeserializeObject(json);
				Boolean isIdentical = result.isIdentical;
				return isIdentical;
			}
			catch (Exception e)
			{
				Console.Write(e.Message);
				return false;
			}
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