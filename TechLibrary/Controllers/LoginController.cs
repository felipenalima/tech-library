using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechLibrary.Communication.Requests;
using TechLibrary.UseCases.Login.Login;

namespace TechLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login(RequestLoginJson request)
        {
            var useCase = new LoginUseCase();
            var response = useCase.Execute(request);
            
            return Ok(response);
        }
    }
}
