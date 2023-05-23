using Microsoft.AspNetCore.Mvc;
using Service.IServices;

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
            var personas = _person.GetEmployees();

            // Enviar correo electrónico
            var recipient = "diegogutcat28@gmail.com";
            var subject = "Hola";
            var body = "Este es un correo de prueba";

            _emailService.SendEmail(recipient, subject, body);

            return Ok(_person.GetEmployees());
        }
    }
}
