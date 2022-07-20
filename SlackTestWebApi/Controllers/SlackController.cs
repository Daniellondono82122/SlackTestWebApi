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
        private readonly IEventsService _eventsService;

        public SlackController(ILogger<SlackController> logger, ISlackService slackService,
            IEventsService eventsService)
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
        public async Task<IActionResult> Event([FromBody] dynamic request)
        {
            try
            {
                if (request is null)
                {
                    throw new BadHttpRequestException("request null");
                }
                switch (request.type)
                {
                    case "url_verification":
                        return Content(request.challenge);

                    case "event_callback":
                        var eventRequest = JsonConvert.DeserializeObject<SlackEventMessage>(request.ToString());
                        BaseResponseDto<SlackResponseDto> response = await _eventsService.Handle(eventRequest);
                        if (response.Errors != null) return NotFound(response.Message);
                        return Ok(response);

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
    }
}
