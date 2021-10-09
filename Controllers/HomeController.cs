using CLED.Areas.Identity.Data;
using CLED.Constants;
using CLED.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using SmtpClient = System.Net.Mail.SmtpClient;

namespace CLED.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<CLEDUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(ILogger<HomeController> logger, UserManager<CLEDUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
     
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

  

        [HttpPost]
        [Route("/ContactUs")]
        public IActionResult ContactUs(SendMailDto sendMailDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("seifeddine.attia.1@esprit.tn");
                mail.To.Add("seifeddine.attia.1@esprit.tn");
                mail.Subject = sendMailDto.Subject;
                mail.IsBodyHtml = true;
                string content = "Name: " + sendMailDto.Name;
                content += "<br> Email: " + sendMailDto.Email;
                content += "<br> Message: " + sendMailDto.Message;
                mail.Body = content;

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");

                NetworkCredential networkCredential = new NetworkCredential("seifeddine.attia.1@esprit.tn","Seifattia1");

                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = int.Parse("587");
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = networkCredential;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Send(mail);
                ViewBag.message = "Mail send";

                ModelState.Clear();
            }
            catch (Exception ex)
            {
                ViewBag.message = "Error: " + ex.Message;
            }
            return View("Index");
        }
       
     

    }
}
