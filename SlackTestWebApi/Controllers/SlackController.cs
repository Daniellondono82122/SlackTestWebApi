using Microsoft.AspNetCore.Mvc;

namespace SlackTestWebApi.Controllers
{
    public class SlackController : Controller
    {
        
        public async Task<IActionResult> Test()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}
