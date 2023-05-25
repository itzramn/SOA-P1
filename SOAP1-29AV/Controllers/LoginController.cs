using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.IServices;

namespace SOAP1_29AV.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IPerson _person;

        public LoginController(IPerson person)
        {
            _person = person;
        }

        [HttpPost]
        public IActionResult Index(string email,string password)
        {
     
           _person.ValidateLogin(email, password);

            bool isVaildUser = _person.ValidateLogin(email, password);

            if (isVaildUser)
            {
                return Ok("todo bien");
            }
            else
            {
                return Ok("nel");
            }
        }
    }
}
