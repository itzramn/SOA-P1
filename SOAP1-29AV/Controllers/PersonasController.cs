using Microsoft.AspNetCore.Mvc;
using Service.IServices;
using System.Net.Mail;
using System.Net.Mime;


namespace SOAP1_29AV.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonasController : Controller
    {

        private readonly IPersona _persona;
        private readonly IEmailService _emailService;


        public PersonasController(IPersona persona, IEmailService emailService)
        {
            _persona = persona;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var personas = _persona.GetEmpleados();

            // Enviar correo electrónico
            var recipient = "diegogutcat28@gmail.com";
            var subject = "Bienvenido";
            var htmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./../../../Welcome.html");
            var htmlBody = System.IO.File.ReadAllText(htmlFilePath);

            // Crear el cuerpo HTML como una vista alternativa
            var htmlView = AlternateView.CreateAlternateViewFromString(htmlBody, new ContentType("text/html"));

            _emailService.SendEmail(recipient, subject, htmlBody);
            _emailService.SendSmtpEmail(recipient, subject, htmlBody);
                    
            return Ok(_persona.GetEmpleados());
        }
    }
}
