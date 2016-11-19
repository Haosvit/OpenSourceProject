using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace OpenSourceProject.Controllers
{
    public class RegisterController : Controller
    {
        private string IMAGE_NAME_KEY = "image_name";
        private string IMAGE_PATH_KEY = "image_path";
        private string IMAGE_DIR = "~/Content/Images/Register/";
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        public void captureRegister1()
        {
            var stream = Request.InputStream;

            string dump;

            string path = "";

            using (var reader = new StreamReader(stream))
            {
                dump = reader.ReadToEnd();

                DateTime nm = DateTime.Now;

                string date = nm.ToString("yyyymmddMMss");


                path = Server.MapPath(IMAGE_DIR + "RegisterImage1.jpg");

                string dir = Directory.GetParent(path).FullName;
                //path = Path.Combine(dir, "user_" + date + ".jpg");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                //System.IO.File.SetAttributes(path, FileAttributes.Normal);

                // TODO: chỗ này t thấy lấy cái byte này gửi lên validate luôn cũng được, khỏi cần lưu
                System.IO.File.WriteAllBytes(path, String_To_Bytes2(dump));

                
            }
        }

        public void captureRegister2()
        {
            var stream = Request.InputStream;

            string dump;

            string path = "";

            using (var reader = new StreamReader(stream))
            {
                dump = reader.ReadToEnd();

                DateTime nm = DateTime.Now;

                string date = nm.ToString("yyyymmddMMss");


                path = Server.MapPath(IMAGE_DIR + "RegisterImage2.jpg");

                string dir = Directory.GetParent(path).FullName;
                //path = Path.Combine(dir, "user_" + date + ".jpg");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                //System.IO.File.SetAttributes(path, FileAttributes.Normal);

                // TODO: chỗ này t thấy lấy cái byte này gửi lên validate luôn cũng được, khỏi cần lưu
                System.IO.File.WriteAllBytes(path, String_To_Bytes2(dump));


            }
        }

        public void captureRegister3()
        {
            var stream = Request.InputStream;

            string dump;

            string path = "";

            using (var reader = new StreamReader(stream))
            {
                dump = reader.ReadToEnd();

                DateTime nm = DateTime.Now;

                string date = nm.ToString("yyyymmddMMss");


                path = Server.MapPath(IMAGE_DIR + "RegisterImage3.jpg");

                string dir = Directory.GetParent(path).FullName;
                //path = Path.Combine(dir, "user_" + date + ".jpg");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                //System.IO.File.SetAttributes(path, FileAttributes.Normal);

                // TODO: chỗ này t thấy lấy cái byte này gửi lên validate luôn cũng được, khỏi cần lưu
                System.IO.File.WriteAllBytes(path, String_To_Bytes2(dump));


            }
        }

        public ActionResult processRegister()
        {

            return RedirectToAction("/Login/Index");
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
