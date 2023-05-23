using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.IServices;
using System.Net.Mail;
using System.Net.Mime;


namespace SOAP1_29AV.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonsController : Controller
    {

        private readonly IPerson _person;
        private readonly IEmailService _emailService;


        public PersonsController(IPerson person, IEmailService emailService)
        {
            _person = person;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var persons = _person.GetEmployees();

            // Enviar correo electrónico
            var subject = "Bienvenido";
            var htmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./../../../Welcome.html");
            var htmlBody = System.IO.File.ReadAllText(htmlFilePath);

            foreach (var person in persons)
            {
                var recipient = person.Email;
                _emailService.SendEmail(recipient, subject, htmlBody);
                _emailService.SendSmtpEmail(recipient, subject, htmlBody);
            }

            return Ok(_person.GetEmployees());
        }
    }
}
