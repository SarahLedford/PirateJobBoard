using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PirateJobBoard.UI.MVC.Models;

namespace PirateJobBoard.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            if (Request.IsAuthenticated)
            {
                ContactViewModel contactModelEmail = new ContactViewModel();
                contactModelEmail.EmailAddress = User.Identity.GetUserName();

                return View(contactModelEmail);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactViewModel contactModel)
        {
            if (!ModelState.IsValid)
            {
                return View(contactModel);
            }
            string message = $"Ye have received a message from {contactModel.Name}<br /><br />" +
                $"Subject: {contactModel.Subject}<br /><br />" +
                $"Reply To: {contactModel.EmailAddress}<br /><br />" +
                $"Message:<br />{contactModel.Message}";

            MailMessage mm = new MailMessage(ConfigurationManager.AppSettings.Get("EmailUser"), ConfigurationManager.AppSettings.Get("EmailToAddress"), contactModel.Subject, message);

            mm.IsBodyHtml = true;
            mm.Priority = MailPriority.High;
            mm.ReplyToList.Add(contactModel.EmailAddress);

            SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings.Get("EmailServer"));
            client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings.Get("EmailUser"), ConfigurationManager.AppSettings.Get("EmailPW"));

            try
            {
                client.Send(mm);
            }
            catch (Exception ex)
            {
                ViewBag.EmailMessage = $"We're sorry. Your request could not be completed at this time. Please" +
                    $" try again later. Error Message: {ex.StackTrace}.";
                return View(contactModel);
            }
            return View("EmailConfirm", contactModel);
        }
    }
}
