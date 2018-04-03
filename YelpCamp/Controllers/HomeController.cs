using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using YelpCamp.App_Start;
using YelpCamp.Models;

namespace YelpCamp.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Contact(FormCollection form)
        {
            string response = form["g-recaptcha-response"];
            const string secret = "6LfP4E0UAAAAAA2QYHfRwYyfKDN8YOsCSZ27VY0c";

            var client = new WebClient();
            var reply =
                client.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}",
                secret, response));

            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);
            if (captchaResponse.Success != "True")
            {
                var error = captchaResponse.ErrorCodes[0].ToLower();
                ViewBag.Error = error;
                return View();

            }
            ViewBag.Error = null;


            var credentialUserName = "moh@gmail.com";
            var pwd = "yourpwd";

            // Configure the client:
            System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com");

            smtpClient.Port = 587;
            smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            // Creatte the credentials:
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(credentialUserName, pwd);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = credentials;

            // Create the message:
            var mail = new System.Net.Mail.MailMessage("moh@gmail.com", "moh@gmail.com");
            mail.Subject = "Let's Camp contact request from: " + form["name"];
            mail.Body = "You have received an email from...Name: " + form["name"] + " Phone: " + form["phone"] + " Email: " + form["email"] + " Message: " + form["message"];


            mail.IsBodyHtml = true;

            // Plug in your email service here to send an email.
           // await smtpClient.SendMailAsync(mail);
            FlashMessage("Your Email Has Send Succefuly", FlashMessageType.success);

            return RedirectToAction("GetAllCampgrounds", "Campground");
            
           

        }
        private void FlashMessage(string message, FlashMessageType Type)
        {
            TempData["Message"] = message;
            TempData["cls"] = Type;
        }
    }
}