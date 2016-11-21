using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Specialized;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data.Entity;

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
        //check đã chụp ảnh chưa
        string path1 = Server.MapPath(IMAGE_DIR + "RegisterImage1.jpg");
        string path2 = Server.MapPath(IMAGE_DIR + "RegisterImage2.jpg");
        string path3 = Server.MapPath(IMAGE_DIR + "RegisterImage3.jpg");

        string dir1 = Directory.GetParent(path1).FullName;
        string dir2 = Directory.GetParent(path2).FullName;
        string dir3 = Directory.GetParent(path3).FullName;

        if (!System.IO.File.Exists(path1) && !System.IO.File.Exists(path2) && !System.IO.File.Exists(path3))
        {
            return RedirectToAction("Register", "Login");
        }
        else
        {
            NameValueCollection form = Request.Form;
            string fullName = "", email = "", personId = "";
            string sex;
            fullName = form["fullName"];
            email = form["email"];
            sex = form["optradio"];
            Debug.WriteLine(fullName + email + sex);


                //Goi hàm kiểm tra email đã tồn tại checkEmail(email); trả về true nếu đã tồn tại trong database
                try
                {
                    Debug.WriteLine(1);
                    //Tao mới person
                    var request =
                        (HttpWebRequest)
                            WebRequest.Create(
                                "https://api.projectoxford.ai/face/v1.0/persongroups/open_source_net1/persons");
                    var postData = "{\"name\":\"" + email + "\",\"userData\":\"user-provided data attached to the person group\"}";
                    //var imgBinary = File.ReadAllBytes(_imgPath1);

                    byte[] byteData = Encoding.UTF8.GetBytes(postData);
                    //var data = Encoding.ASCII.GetBytes(postData);

                    request.Method = "POST";
                    request.ContentType = "application/json";
                    request.ContentLength = byteData.Length;
                    request.Host = "api.projectoxford.ai";
                    request.Headers.Add("Ocp-Apim-Subscription-Key", "1c056c36ece84f14a0619803ee4f0ceb");

                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(byteData, 0, byteData.Length);
                    }

                    var response = (HttpWebResponse)request.GetResponse();

                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    string json = responseString.ToString();
                    dynamic stuff = JObject.Parse(json);
                    personId = stuff.personId;

                    //Vi - TODO: goi ham luu person moi vao database addPerson(fullName, email, personId);



                    //add ảnh vào person
                    if (Directory.Exists(dir1))
                    {
                        Debug.WriteLine(2);
                        var request1 =
                        (HttpWebRequest)
                            WebRequest.Create(
                                "https://api.projectoxford.ai/face/v1.0/persongroups/open_source_net1/persons/" + personId + "/persistedFaces");

                        Debug.WriteLine(path1);
                        var imgBinary = System.IO.File.ReadAllBytes(path1);


                        request1.Method = "POST";
                        request1.ContentType = "application/octet-stream";
                        request1.ContentLength = imgBinary.Length;
                        request1.Host = "api.projectoxford.ai";
                        request1.Headers.Add("Ocp-Apim-Subscription-Key", "1c056c36ece84f14a0619803ee4f0ceb");

                        using (var stream = request1.GetRequestStream())
                        {
                            stream.Write(imgBinary, 0, imgBinary.Length);
                        }

                        var response1 = (HttpWebResponse)request1.GetResponse();

                        responseString = new StreamReader(response1.GetResponseStream()).ReadToEnd();
                        json = responseString.ToString();
                        Debug.WriteLine(json);
                        stuff = JObject.Parse(json);

                        string persistedFaceId = stuff.persistedFaceId;
                        Debug.WriteLine(persistedFaceId);
                        //addIdentification(userId, persistedFaceId)
                    }
                    if (System.IO.File.Exists(path2))
                    {
                        Debug.WriteLine(Directory.Exists(dir2));
                        var request1 =
                        (HttpWebRequest)
                            WebRequest.Create(
                                "https://api.projectoxford.ai/face/v1.0/persongroups/open_source_net1/persons/" + personId + "/persistedFaces");


                        var imgBinary = System.IO.File.ReadAllBytes(path2);

                        request1.Method = "POST";
                        request1.ContentType = "application/octet-stream";
                        request1.ContentLength = imgBinary.Length;
                        request1.Host = "api.projectoxford.ai";
                        request1.Headers.Add("Ocp-Apim-Subscription-Key", "1c056c36ece84f14a0619803ee4f0ceb");

                        using (var stream = request1.GetRequestStream())
                        {
                            stream.Write(imgBinary, 0, imgBinary.Length);
                        }

                        var response1 = (HttpWebResponse)request1.GetResponse();

                        responseString = new StreamReader(response1.GetResponseStream()).ReadToEnd();
                        json = responseString.ToString();
                        stuff = JObject.Parse(json);

                        string persistedFaceId = stuff.persistedFaceId;
                        Debug.WriteLine(persistedFaceId);
                        //addIdentification(userId, persistedFaceId)
                    }
                    if (System.IO.File.Exists(path3))
                    {
                        Debug.WriteLine(4);
                        var request1 =
                        (HttpWebRequest)
                            WebRequest.Create(
                                "https://api.projectoxford.ai/face/v1.0/persongroups/open_source_net1/persons/" + personId + "/persistedFaces");


                        var imgBinary = System.IO.File.ReadAllBytes(path3);

                        request1.Method = "POST";
                        request1.ContentType = "application/octet-stream";
                        request1.ContentLength = imgBinary.Length;
                        request1.Host = "api.projectoxford.ai";
                        request1.Headers.Add("Ocp-Apim-Subscription-Key", "1c056c36ece84f14a0619803ee4f0ceb");

                        using (var stream = request1.GetRequestStream())
                        {
                            stream.Write(imgBinary, 0, imgBinary.Length);
                        }

                        var response1 = (HttpWebResponse)request1.GetResponse();

                        responseString = new StreamReader(response1.GetResponseStream()).ReadToEnd();
                        json = responseString.ToString();
                        stuff = JObject.Parse(json);

                        string persistedFaceId = stuff.persistedFaceId;
                        Debug.WriteLine(persistedFaceId);
                        //addIdentification(userId, persistedFaceId)
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                    Debug.WriteLine(ex.Message);
                    return RedirectToAction("Register", "Login");
                }

        }
            


        return RedirectToAction("Index","Login");
    }

    public ActionResult CheckLogin()
    {


        return RedirectToAction("Index", "Home");
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
