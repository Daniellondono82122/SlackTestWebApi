namespace SlackTestWebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Domain.Dtos;
    using Services.Services;

    [ApiController]
    [Route("api/[controller]")]
    public class SlackController : ControllerBase
    {
        private readonly ILogger<SlackController> _logger;
        private readonly ISlackService _slackService;

        public SlackController(ILogger<SlackController> logger, ISlackService slackService)
        {
            _logger = logger;
            _slackService = slackService;
        }


        [HttpPost(Name = "SendMessage")]
        public async Task<IActionResult> SendMessageAsync([FromBody] PayloadMessage payloadMessage)
        {
            try
            {
                var response = await _slackService.SendMessageAsync(payloadMessage);
                if (response.Errors != null) return NotFound(response.Message);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
