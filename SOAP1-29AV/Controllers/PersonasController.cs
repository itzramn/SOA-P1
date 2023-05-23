using Microsoft.AspNetCore.Mvc;
using Service.IServices;

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
            var subject = "Hola";
            var body = "Este es un correo de prueba";

            _emailService.SendEmail(recipient, subject, body);

            return Ok(_persona.GetEmpleados());
        }
    }
}
