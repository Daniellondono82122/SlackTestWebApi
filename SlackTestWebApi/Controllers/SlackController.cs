namespace SlackTestWebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Domain.Dtos;
    using Services.Services;
    using Newtonsoft.Json;
    using SlackTestWebApi.Domain.Dtos.Slack;

    [ApiController]
    [Route("api/[controller]")]
    public class SlackController : ControllerBase
    {
        private readonly ILogger<SlackController> _logger;
        private readonly ISlackService _slackService;
        private readonly IEventService _eventsService;

        public SlackController(ILogger<SlackController> logger, ISlackService slackService,
            IEventService eventsService)
        {
            _logger = logger;
            _slackService = slackService;
            _eventsService = eventsService;
        }


        [HttpPost("SendMessage")]
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

        [HttpPost("Event")]
        public async Task<IActionResult> Event([FromBody] object request)
        {
            try
            {
                if (request is null)
                {
                    throw new BadHttpRequestException("request null");
                }

                var validationMessage = JsonConvert.DeserializeObject<ValidationMessage>(request.ToString());

                switch (validationMessage.Type)
                {
                    case "url_verification":
                        return Content(validationMessage.Challenge);

                    case "event_callback":
                        var eventRequest = JsonConvert.DeserializeObject<SlackEventMessage>(request.ToString());
                        await _eventsService.ProcessUserMessage(eventRequest);
                        return Ok();

                    default:
                        throw new BadHttpRequestException(JsonConvert.SerializeObject(request));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("GetUserIdsByEmail")]
        public async Task<IActionResult> GetUserIdsByEmail([FromBody] List<string> emails)
        {
            try
            {
                BaseResponseDto<List<ExternalUserDto>> response = await _slackService.GetUsersByEmailAsync(emails);
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
